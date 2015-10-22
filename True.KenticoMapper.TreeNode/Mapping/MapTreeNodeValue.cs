using System;
using System.Reflection;
using True.KenticoMapper.Core;
using True.KenticoMapper.Core.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Mapping
{
    public class MapTreeNodeValue : IMapValue
    {
        private readonly IKenticoTreeNode _source;
        private readonly INamedSyncIn _attibute;

        public MapTreeNodeValue(IKenticoTreeNode source, INamedSyncIn attribute)
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