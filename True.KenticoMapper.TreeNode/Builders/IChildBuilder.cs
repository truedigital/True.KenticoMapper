using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public interface IChildBuilder
    {
        object Build(IKenticoTreeNode source, ISyncInChild attribute, ITreeNodeMapper mapper);
    }
}