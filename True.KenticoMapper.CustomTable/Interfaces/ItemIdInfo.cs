using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.CustomTable.Interfaces
{
    /// <summary>
    /// Unique identifier available on all custom data table items for accessing single record
    /// </summary>
    public interface IItemIdInfo : IItemGuidInfo
    {
        [SyncInId]
        int ItemId { get; set; }
    }
}
