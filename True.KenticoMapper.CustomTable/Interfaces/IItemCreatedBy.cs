namespace True.KenticoMapper.CustomTable.Interfaces
{
    /// <summary>
    /// Id of user who created this item
    /// </summary>
    public interface IItemCreatedBy
    {
        int? ItemCreatedBy { get; set; }
    }
}