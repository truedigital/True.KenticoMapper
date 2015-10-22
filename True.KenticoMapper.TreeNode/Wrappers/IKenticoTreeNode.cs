using System.Collections.Generic;

namespace True.KenticoMapper.TreeNode.Wrappers
{
    public interface IKenticoTreeNode
    {
        CMS.DocumentEngine.TreeNode TreeNode { get; }
        string Name { get; }
        int Id { get; }
        int ParentId { get; }
        string RelativeUrl { get; }
        string NodeClassName { get; }
        IEnumerable<IKenticoTreeNode> Children { get; }
        object this[string columnName] { get; }
    }
}