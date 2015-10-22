using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public interface IChildrenBuilder
    {
        object Build(IKenticoTreeNode source, ISyncInChildren attribute, ITreeNodeMapper mapper);
    }
}