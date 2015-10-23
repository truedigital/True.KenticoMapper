# Kentico Mapper

A set of assemblies to simplify mapping from Kentico TreeNode and CustomTable objects to your own custom types. 
These assemblies have been built against Kentico v8.2 under Visual Studio 2015. 
I cannot guarantee functionality will work against earlier versions of Kentico, although development of these components did start against Kentico v7.0 .

## Contents

1. [Info](#info)
2. [Quick Start](#quick-start)
3. [Custom Table Mapping](#custom-table-mapping)
3. [Custom Table Attributes](#custom-table-attributes)
4. [Tree Node Mapping](#tree-node-mapping)
4. [Tree Node Attributes](#tree-node-attributes)

## Info

- Can map CustomTable objects to custom types of your definition, with the ability to persist changes in the database
- Can map TreeNode objects to custom types of your definition, including mapping child page types, referenced page types and complex elements within a page
- Uses attribute decoration to denote mappable properties

## Quick Start

1. Clone the repo (or fork it)

        git clone https://github.com/truedigital/True.KenticoMapper.git

2. Install:

        Include the assemblies to your Kentico v8.2 project and reference them where appropriate

		
## User guide

## Custom Table Mapping

The CustomTableMapper can both read from and write to an associated custom table, if the correct attribute decorations are used

1. Create a custom class that you want to model a custom table

		public class PersonInfo
		{
			public int ItemId { get; set; }
			public string FirstName { get; set; }
			public string SecondName { get; set; }
			public string Title { get; set; }
			public int? Age { get; set; }
		}
		
2. Decorate all properties with the appropriate attributes

		public class PersonInfo : IItemIdInfo
		{
			[SyncInId]
			public int ItemId { get; set; }

			[SyncInOut("FirstName")]
			public string FirstName { get; set; }

			[SyncInOut("LastName")]
			public string LastName { get; set; }

			[SyncInOut("Title")]
			public string Title { get; set; }
			
			[SyncInOut("Age")]
			public int? Age { get; set; }
		}
		
3. Use the CustomTableMapper to allow you to interact with a custom table via your custom objects

		public class PersonInfoProvider
		{
			public PersonInfo GetPersonInfo(int itemId)
			{
				var provider = new CustomTableMapper("True.Person");

				var where = string.Format("ItemID = {0}", itemId);
				return provider.Get<PersonInfo>(where);
			}

			public void SetPersonInfo(PersonInfo info)
			{
				var provider = new CustomTableMapper("True.Person");
				provider.Set(info);
			}
		}
		
## Custom Table Attibutes

		[SyncIgnore]
Tells the mapper to ignore this property

		[SyncInId]
Tells the mapper to read in the node id the type that this attribute decorates MUST be an int

		[SyncInGuid]
Tells the mapper to read in the node id the type that this attribute decorates MUST be a Guid

		[SyncIn("column_name")]
Tells the mapper to read from the column_name value, the type that this attribute decorates MUST be the C# equivalent of the SQL type, including nullable types

		[SyncOut("column_name")]
Tells the mapper that it can write to the column_name value, the type that this attribute decorates MUST be the C# equivalent of the SQL type, including nullable types

		[SyncInOut("column_name")]
Tells the mapper that it can read from and write to the column_name value, the type that this attribute decorates MUST be the C# equivalent of the SQL type, including nullable types


## Tree Node Mapping

NB. The TreeNodeMapper currently only reads from TreeNodes, it cannot write to them. 

1. Create a custom class that you want to model a custom table
    
		public interface IBook
		{
			int BookId { get; set; }
			IAuthor Author { get; set; }
			string ISBN { get; set; }
		}
		
		public class Book : IBook
		{
			public int BookId { get; set; }
			public IAuthor Author { get; set; }
			public string ISBN { get; set; }
		}
		
		public interface IAuthor
		{
			string FirstName { get; set; }
			string LastName { get; set; }
			string Summary { get; set; }
		}

		public class Author : IAuthor
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Summary { get; set; }
		}
		
2. Decorate all properties with the appropriate attributes
	
		public class Book : IBook
		{
			[SyncInId]
			public int BookId { get; set; }

			[SyncInComplex("Author", typeof (ComplexBuilder<Author, IAuthor>))]
			public IAuthor Author { get; set; }

			[SyncIn("ISBN")]
			public string ISBN { get; set; }
		}

		public class Author : IAuthor
		{
			[SyncIn("FirstName")]
			public string FirstName { get; set; }

			[SyncIn("LastName")]
			public string LastName { get; set; }

			[SyncIn("Summary")]
			public string Summary { get; set; }
		}
		
3. Use the TreeNodeMapper to allow you to interact with a custom table via your custom objects

		public class BookProvider
		{
			public Book GetBook(TreeNode node)
			{
				var provider = new TreeNodeMapper();
				return provider.Create<Book>(new KenticoTreeNode(node));
			}
		}

## Tree Node Attributes

		[SyncIgnore]
Tells the mapper to ignore this property

		[SyncInId]
Tells the mapper to read in the node id the type that this attribute decorates MUST be an int

		[SyncInParentId]
Tells the mapper to read in the parent node id, the type that this attribute decorates MUST be an int

		[SyncInDocumentName]
Tells the mapper to read in the document name, the type that this attribute decorates MUST be a string

		[SyncInRelativeUrl]
Tells the mapper to read in the relative url, the type that this attribute decorates MUST be a string

		[SyncInNodeClassName]
Tells the mapper to read in the node class name, the type that this attribute decorates MUST be a string

		[SyncInCollection("column_name", delimiter : "|")]
Tells the mapper to create an object of type IEnumerable<string> split on the delimiter from the value stored in column_name

		[SyncIn("column_name")]
Tells the mapper to read from the column_name value, the type that this attribute decorates MUST be the C# equivalent of the SQL type, including nullable types

		[SyncInComplex(typeof(ComplexBuilder<T, TInterface>))] 
Tells the mapper to create an object of type TInterface and map its fields from the current tree node

		[SyncInComplex("column_name", typeof(ComplexBuilder<T, TInterface>))] 
Tells the mapper to create an object of type TInterface and map its fields from the tree node of the node id stored in column_name

		[SyncInComplexCollection("column_name", typeof(ComplexCollectionBuilder<T, TInterface>))] 
Tells the mapper to create an object of type IEnumerable<TInterface>  and map its fields from the list of tree nodes of the of the node ids stored in column_name

		[SyncInChildren(typeof (ChildrenBuilder<T, TInterface>))] 
Tells the mapper to create an object of type IEnumerable<TInterface> and map its children out as instances of type TInterface
	
		[SyncInChild(typeof(ChildBuilder<T, TInterface>))] 
Tells the mapper to create an object of type TInterface and map its first child out as an instance of type TInterface
		
## Changelog

You can keep up-to-date with the changes that we have made via our [releases page](https://github.com/truedigital/True.KenticoMapper/releases).

## License

Code released under the [MIT license](https://github.com/truedigital/True.KenticoMapper/blob/master/LICENSE). Documentation released under [Creative Commons](http://creativecommons.org/licenses/by-sa/4.0/).