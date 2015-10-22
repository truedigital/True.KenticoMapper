using System.Linq;
using CMS.DocumentEngine;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public class ChildrenBuilder<T, TResult> : IChildrenBuilder
        where T : class, TResult, new()
        where TResult : class
    {
        public object Build(IKenticoTreeNode source, ISyncInChildren attribute, ITreeNodeMapper mapper)
        {
            var children = source.Children;
            if (children == null)
                return new T();

            var provider = new TreeProvider();

            return children
                .Select(s => provider.SelectSingleNode(s.Id))
                .Where(s => s != null)
                .Select(s => mapper.Create<T>(new KenticoTreeNode(s)))
                .ToList();
        }
    }

    public class ChildrenBuilder<T> : ChildrenBuilder<T, T>
        where T : class, new()
    {
    }
}