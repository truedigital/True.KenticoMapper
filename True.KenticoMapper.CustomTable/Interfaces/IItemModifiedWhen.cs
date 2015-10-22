using System;

namespace True.KenticoMapper.CustomTable.Interfaces
{
    /// <summary>
    /// Time of last modification of this item
    /// </summary>
    public interface IItemModifiedWhen
    {
        DateTime? ItemModifiedWhen { get; set; }
    }
}