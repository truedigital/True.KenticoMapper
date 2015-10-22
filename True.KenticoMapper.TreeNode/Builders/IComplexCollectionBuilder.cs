using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public interface IComplexCollectionBuilder
    {
        object Build(IKenticoTreeNode source, ISyncInComplexCollection attribute, ITreeNodeMapper mapper);
    }
}