using System;
using System.Reflection;
using True.KenticoMapper.Core;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Mapping
{
    public class MapTreeNodeCollection : IMapValue
    {
        private readonly IKenticoTreeNode _source;
        private readonly ISyncInCollection _attibute;

        public MapTreeNodeCollection(IKenticoTreeNode source, ISyncInCollection attribute)
        {
            _source = source;
            _attibute = attribute;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source[_attibute.Name] != DBNull.Value ? _source[_attibute.Name] : null;
            if (value == null)
                return;

            var values = value.ToString().Split(new[] {_attibute.Delimiter}, StringSplitOptions.RemoveEmptyEntries);
            property.SetValue(target, values, null);
        }
    }
}