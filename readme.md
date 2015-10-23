# Kentico Mapper

A set of assemblies to simplify mapping from Kentico TreeNode and CustomTable objects to your own custom types. 
These assemblies have been built against Kentico v8.2 under Visual Studio 2015.  

## Contents

1. [Info](#info)
2. [Quick Start](#quick-start)
2. [User Guide](#user-guide)
2. [Custom Table Mapping](#custom-table-mapping)
2. [Tree Node Mapping](#tree-node-mapping)

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

1. Create a custom class that you want to model a custom table

		public class PersonInfo
		{
			public int ItemId { get; set; }
			public string FirstName { get; set; }
			public string SecondName { get; set; }
			public string Title { get; set; }
			public int Age { get; set; }
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
			public int Age { get; set; }
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

## Tree Node Mapping
	
		
## Changelog

You can keep up-to-date with the changes that we have made via our [releases page](https://github.com/truedigital/True.KenticoMapper/releases).

## License

Code released under the [MIT license](https://github.com/truedigital/True.KenticoMapper/blob/master/LICENSE). Documentation released under [Creative Commons](http://creativecommons.org/licenses/by-sa/4.0/).