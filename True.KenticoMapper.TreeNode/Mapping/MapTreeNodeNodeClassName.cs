using System.Reflection;
using True.KenticoMapper.Core;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Mapping
{
    public class MapTreeNodeNodeClassName : IMapValue
    {
        private readonly IKenticoTreeNode _source;

        public MapTreeNodeNodeClassName(IKenticoTreeNode source)
        {
            _source = source;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source.NodeClassName;
            property.SetValue(target, value, null);
        }
    }
}