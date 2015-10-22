using System.Reflection;

namespace True.KenticoMapper.Core.Mapping
{
    public class MapIndexerGuid : IMapValue
    {
        private readonly IIndexer _source;

        public MapIndexerGuid(IIndexer source)
        {
            _source = source;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source["ItemGUID"];
            property.SetValue(target, value, null);
        }
    }
}
