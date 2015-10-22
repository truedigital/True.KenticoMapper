using System;
using True.KenticoMapper.TreeNode.Builders;

namespace True.KenticoMapper.TreeNode.Attributes
{
    /// Attribute that should be used to decorate a type that should read information in from the database
    /// </summary>
    public class SyncInComplexCollection : Attribute, ISyncInComplexCollection
    {
        public string Name { get; }
        public Type Type { get; }
        public char Delimiter { get; set; }

        public SyncInComplexCollection(string columnName, Type type, char delimiter = '|')
        {
            var interfaceType = typeof(IComplexCollectionBuilder);
            if (interfaceType.IsAssignableFrom(type) == false)
            {
                throw new Exception(string.Format("{0} must implement {1}", type.Name, interfaceType.Name));
            }

            Name = columnName;
            Type = type;
            Delimiter = delimiter;
        }
    }
}