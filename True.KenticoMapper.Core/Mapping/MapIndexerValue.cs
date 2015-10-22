using System;
using System.Reflection;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.Core.Mapping
{
    public class MapIndexerValue : IMapValue
    {
        private readonly IIndexer _source;
        private readonly INamedSyncIn _attibute;

        public MapIndexerValue(IIndexer source, INamedSyncIn attribute)
        {
            _source = source;
            _attibute = attribute;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source[_attibute.Name] != DBNull.Value ? _source[_attibute.Name] : null;
            property.SetValue(target, value, null);
        }
    }
}
