namespace True.KenticoMapper.CustomTable.Interfaces
{
    /// <summary>
    /// Id of user who last modified of this item
    /// </summary>
    public interface IItemModifiedBy
    {
        int? ItemModifiedBy { get; set; }
    }
}