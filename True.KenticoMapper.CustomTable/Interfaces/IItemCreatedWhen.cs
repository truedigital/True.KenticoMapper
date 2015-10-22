using System;

namespace True.KenticoMapper.CustomTable.Interfaces
{
    /// <summary>
    /// Time of creation of this item
    /// </summary>
    public interface IItemCreatedWhen
    {
        DateTime? ItemCreatedWhen { get; set; }
    }
}