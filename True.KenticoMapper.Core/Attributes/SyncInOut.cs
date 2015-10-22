using System;

namespace True.KenticoMapper.Core.Attributes
{
    /// <summary>
    /// Attribute that should be used to decorate a type that should read information in from the database and write information out to the database
    /// </summary>
    public class SyncInOut : Attribute, INamedSyncIn, INamedSyncOut
    {
        public string Name { get; }

        public SyncInOut(string columnName)
        {
            Name = columnName;
        }
    }
}