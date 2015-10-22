using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace True.KenticoMapper.TreeNode.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class KenticoTreeNode : IKenticoTreeNode
    {
        public KenticoTreeNode(CMS.DocumentEngine.TreeNode node)
        {
            TreeNode = node;
        }

        public CMS.DocumentEngine.TreeNode TreeNode { get; }

        public string Name => TreeNode.DocumentName;
        public int Id => TreeNode.NodeID;
        public int ParentId => TreeNode.NodeParentID;
        public string RelativeUrl => TreeNode.RelativeURL;
        public string NodeClassName => TreeNode.NodeClassName;

        public IEnumerable<IKenticoTreeNode> Children
        {
            get { return TreeNode.Children.AsEnumerable().Select(s => (IKenticoTreeNode)new KenticoTreeNode(s)); }
        }

        public object this[string columnName] => TreeNode[columnName];
    }
}