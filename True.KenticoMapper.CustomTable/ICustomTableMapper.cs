using System.Collections.Generic;
using True.KenticoMapper.CustomTable.Interfaces;

namespace True.KenticoMapper.CustomTable
{
    public interface ICustomTableMapper
    {
        T Get<T>(int itemId) where T : class, IItemIdInfo, new();
        T Get<T>(string where, string orderBy = null) where T : class, IItemIdInfo, new();
        IEnumerable<T> GetAll<T>(string where = null, string orderBy = null) where T : class, IItemIdInfo, new();
        void Set<T>(T info) where T : IItemIdInfo;
        void Delete<T>(T info) where T : IItemIdInfo;
    }
}