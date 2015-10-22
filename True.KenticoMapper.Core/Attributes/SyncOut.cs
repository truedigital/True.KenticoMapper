using System;

namespace True.KenticoMapper.Core.Attributes
{
    /// <summary>
    /// Attribute that should be used to decorate a type that should write information out to the database
    /// </summary>
    public class SyncOut : Attribute, INamedSyncOut
    {
        public string Name { get; }

        public SyncOut(string columnName)
        {
            Name = columnName;
        }
    }
}