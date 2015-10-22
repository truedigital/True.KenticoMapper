using System.Reflection;
using True.KenticoMapper.Core;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Mapping
{
    public class MapTreeNodeId : IMapValue
    {
        private readonly IKenticoTreeNode _source;

        public MapTreeNodeId(IKenticoTreeNode source)
        {
            _source = source;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source.Id;
            property.SetValue(target, value, null);
        }
    }
}