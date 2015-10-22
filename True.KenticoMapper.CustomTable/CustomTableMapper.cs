using System;
using System.Collections.Generic;
using System.Linq;
using CMS.CustomTables;
using CMS.Helpers;
using True.KenticoMapper.Core;
using True.KenticoMapper.CustomTable.Interfaces;

namespace True.KenticoMapper.CustomTable
{
    /// <summary>
    /// Provider class that wraps up all CustomTableItemProvider interactions to allow for user types that mimic Kentico Info objects
    /// </summary>
    public sealed class CustomTableMapper : ICustomTableMapper
    {
        private readonly string _className;
        private readonly IInputMapper _inputMapper;
        private readonly IOutputMapper _outputMapper;

        public CustomTableMapper(string customTableClassName):
            this(customTableClassName, new InputMapper(), new OutputMapper())
        {
        }

        public CustomTableMapper(string customTableClassName, IInputMapper inputMapper, IOutputMapper outputMapper)
        {
            _className = customTableClassName;
            _inputMapper = inputMapper;
            _outputMapper = outputMapper;
        }

        /// <summary>
        /// Gets a mapped item from the database by id
        /// </summary>
        /// <param name="itemId">Id of the item to be mapped from the database</param>
        public T Get<T>(int itemId) where T : class, IItemIdInfo, new()
        {
            var data = CustomTableItemProvider.GetItem(itemId, _className);

            if (DataHelper.IsEmpty(data))
                return default(T);

            var indexer = new Indexer(n => data[n], (n, v) => data[n] = v);
            return Create<T>(indexer);
        }

        /// <summary>
        /// Gets a mapped item from the database by specific matching clauses
        /// </summary>
        /// <param name="where">Optional parameter to select subsets of matching elements</param>
        /// <param name="orderBy">Optional parameter to specify default item sorting</param>
        public T Get<T>(string where, string orderBy = null) where T : class, IItemIdInfo, new()
        {
            var data = CustomTableItemProvider.GetItems(_className, where, orderBy);
            if (DataHelper.IsEmpty(data))
                return default(T);

            var count = data.Count;
            if (count > 1)
                throw new Exception(string.Format("Non-singular request, query (where = \"{0}\") returned {1} results. Additional information is required in query (where = \"{0}\") to return a single result.", where, count));

            if (count < 1)
                return default(T);

            var item = data.FirstObject;
            var indexer = new Indexer(n => item[n], (n, v) => item[n] = v);
            return Create<T>(indexer);
        }

        /// <summary>
        /// Gets an enumeration of mapped items from the database by specific matching clauses
        /// </summary>
        /// <param name="where">Optional parameter to select subsets of matching elements</param>
        /// <param name="orderBy">Optional parameter to specify default item sorting</param>
        public IEnumerable<T> GetAll<T>(string where = null, string orderBy = null) where T : class, IItemIdInfo, new()
        {
            var data = CustomTableItemProvider.GetItems(_className, where, orderBy);
            if (DataHelper.IsEmpty(data))
                yield break;

            foreach (var item in data.AsEnumerable())
            {
                //fix for closure on row being interpreted differently across compilers (i.e. possible closure over reference when closure over value is implied)
                var item1 = item;
                var item2 = item;
                var indexer = new Indexer(n => item1[n], (n, v) => item2[n] = v);
                yield return Create<T>(indexer);
            }
        }

        /// <summary>
        /// Creates or updates a CustomTableItem based on the existence of a valid ItemId in a T object and uses reflection to set its fields
        /// </summary>
        /// <param name="info">Mapped item to be created or updated in the database</param>
        public void Set<T>(T info) where T : IItemIdInfo
        {
            var item = GetItem(info, _className);
            if (DataHelper.IsEmpty(item))
                return;

            var indexer = new Indexer(n => item[n], (n, v) => item[n] = v);
            _outputMapper.GetProperties(info, indexer);

            SetInfo(info, item);
        }
       
        /// <summary>
        /// Deletes an object if a matching CustomTableItem can be found in the selected custom table
        /// </summary>
        /// <param name="info">Mapped item to be deleted from the database</param>
        public void Delete<T>(T info) where T : IItemIdInfo
        {
            var data = GetItem(info, _className);
            if (DataHelper.IsEmpty(data))
                return;

            data.Delete();
        }

        /// <summary>
        /// Creates or gets a CustomTableItem based on the existence of a valid ItemId
        /// </summary>
        /// <param name="info">Mapped item that determines the existence of an item in the database</param>
        /// <param name="className">Class name of the table</param>
        /// <returns>New or existing CustomTableItem depending on the input</returns>
        private static CustomTableItem GetItem<T>(T info, string className) where T : IItemIdInfo
        {
            if (info.ItemId == 0)
                return CustomTableItem.New(className);

            return CustomTableItemProvider.GetItem(info.ItemId, className);
        }

        /// <summary>
        /// Constructs an object and uses reflection to populate its fields from the database
        /// </summary>
        /// <param name="indexer">Object that wraps up an object that implements an indexer into a generic reusable type</param>
        /// <returns>A new object of type T</returns>
        private T Create<T>(IIndexer indexer) where T : class, new()
        {
            var info = new T();
            _inputMapper.SetProperties(indexer, info);
            return info;
        }

        /// <summary>
        /// Inserts or updates an existing CustomTableItem based on the existence of a valid ItemId
        /// </summary>
        /// <param name="info">Mapped item that determines the existence of an item in the database</param>
        /// <param name="item">Custom table item to be processed</param>
        private static void SetInfo<T>(T info, CustomTableItem item) where T : IItemIdInfo
        {
            if (info.ItemId == 0)
            {
                item.Insert();
                info.ItemId = item.ItemID;
                info.ItemGuid = item.ItemGUID;
            }
            else
            {
                item.Update();
            }
        }
    }
}