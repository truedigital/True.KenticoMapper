using System.Reflection;
using True.KenticoMapper.Core;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Mapping
{
    public class MapTreeNodeParentId : IMapValue
    {
        private readonly IKenticoTreeNode _source;

        public MapTreeNodeParentId(IKenticoTreeNode source)
        {
            _source = source;
        }

        public void Set(PropertyInfo property, object target)
        {
            var value = _source.ParentId;
            property.SetValue(target, value, null);
        }
    }
}