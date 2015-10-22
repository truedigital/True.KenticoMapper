using System;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.CustomTable.Interfaces
{
    /// <summary>
    /// Unique identifier available on custom table items that choose to use it
    /// </summary>
    public interface IItemGuidInfo
    {
        [SyncInGuid]
        Guid ItemGuid { get; set; }
    }
}