using System;

namespace True.KenticoMapper.TreeNode.Attributes
{
    /// <summary>
    /// Attribute that should be used to decorate a type that should read information in from the database
    /// </summary>
    public class SyncInCollection : Attribute, ISyncInCollection
    {
        public string Name { get; }
        public char Delimiter { get; set; }

        public SyncInCollection(string columnName, char delimiter)
        {
            Name = columnName;
            Delimiter = delimiter;
        }
    }
}