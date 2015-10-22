using System;

namespace True.KenticoMapper.Core.Attributes
{
    /// <summary>
    /// Attribute that should be used to decorate a type that should read information in from the database
    /// </summary>
    public class SyncIn : Attribute, INamedSyncIn
    {
        public string Name { get; }

        public SyncIn(string columnName)
        {
            Name = columnName;
        }
    }   
    
    /// <summary>
    /// Attribute that should be used to decorate a type that should read information in from the database
    /// </summary>
    public class SyncInImage : Attribute, INamedSyncIn
    {
        public string Name { get; }

        public SyncInImage(string columnName)
        {
            Name = columnName;
        }
    }

    /// <summary>
    /// Attribute that should be used to decorate a type that should read id information in from the database
    /// </summary>
    public class SyncInGuid : Attribute, ISyncIn { }

    /// <summary>
    /// Attribute that should be used to decorate a type that should read name information in from the database
    /// </summary>
    public class SyncInDocumentName : Attribute, ISyncIn { }

    /// <summary>
    /// Attribute that should be used to decorate a type that should read id information in from the database
    /// </summary>
    public class SyncInId : Attribute, ISyncIn { }

    /// <summary>
    /// Attribute that should be used to decorate a type that should read parent id information in from the database
    /// </summary>
    public class SyncInParentId : Attribute, ISyncIn { }

    /// <summary>                                                    
    /// Attribute that should be used to decorate a type that should read relative url information in from the database
    /// </summary>
    public class SyncInRelativeUrl : Attribute, ISyncIn { }

    /// <summary>                                                    
    /// Attribute that should be used to decorate a type that should read document class name information in from the database
    /// </summary>
    public class SyncInNodeClassName : Attribute, ISyncIn { }

}
