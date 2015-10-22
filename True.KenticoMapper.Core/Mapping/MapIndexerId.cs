using System.Reflection;

namespace True.KenticoMapper.Core.Mapping
{
    class MapIndexerId : IMapValue
    {
        private readonly IIndexer _source;

        public MapIndexerId(IIndexer source)
        {
            _source = source;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source["ItemID"];
            property.SetValue(target, value, null);
        }
    }
}
