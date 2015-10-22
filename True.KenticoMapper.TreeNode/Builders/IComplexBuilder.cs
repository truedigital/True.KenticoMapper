using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public interface IComplexBuilder
    {
        object Build(IKenticoTreeNode source, ISyncInComplex attribute, ITreeNodeMapper mapper);
    }
}