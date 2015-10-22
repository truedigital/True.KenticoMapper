using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode
{
    public interface ITreeNodeMapper
    {
        T Create<T>(IKenticoTreeNode source) where T : class, new();
        void Map<T>(IKenticoTreeNode source, T target) where T : class;
    }
}