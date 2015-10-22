using System.Linq;
using CMS.DocumentEngine;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public class ChildBuilder<T> : ChildBuilder<T, T>
        where T : class, new()
    {
    }

    public class ChildBuilder<T, TResult> : IChildBuilder
        where T : class, TResult, new()
        where TResult : class
    {
        public object Build(IKenticoTreeNode source, ISyncInChild attribute, ITreeNodeMapper mapper)
        {
            var child = source.Children.FirstOrDefault();
            if (child == null)
                return new T();

            var provider = new TreeProvider();
            var childNode = provider.SelectSingleNode(child.Id);
            if (childNode == null)
                return new T();

            return mapper.Create<T>(new KenticoTreeNode(childNode));
        }
    }
}