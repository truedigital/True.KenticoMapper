using System;
using System.Collections.Generic;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.Core.Mapping
{
    class SyncInIndexerProvider
    { 
        private readonly Dictionary<Type, Func<IIndexer, ISyncIn, IMapValue>> _mappers = new Dictionary<Type, Func<IIndexer, ISyncIn, IMapValue>>
        {
            {typeof(SyncIn), (item, attribute) => new MapIndexerValue(item, attribute as INamedSyncIn)},
            {typeof(SyncInOut), (item, attribute) => new MapIndexerValue(item, attribute as INamedSyncIn)},
            {typeof(SyncInId), (item, attribute) => new MapIndexerId(item)},
            {typeof(SyncInGuid), (item, attribute) => new MapIndexerGuid(item)}
        };

        public IMapValue GetMapper(IIndexer item, ISyncIn attribute)
        {
            Func<IIndexer, ISyncIn, IMapValue> mapper;
            if (_mappers.TryGetValue(attribute.GetType(), out mapper))
            {
                return mapper(item, attribute);
            }

            return null;
        }
    }
}
