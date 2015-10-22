using CMS.DocumentEngine;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public class ComplexBuilder<T, TResult> : IComplexBuilder
        where T : class, TResult, new()
        where TResult : class
    {
        public object Build(IKenticoTreeNode source, ISyncInComplex attribute, ITreeNodeMapper mapper)
        {
            if (string.IsNullOrEmpty(attribute.Name))
            {
                //if no attribute name is found, map from the current object
                return mapper.Create<T>(source);
            }

            var value = source[attribute.Name];
            if (value == null)
                return new T();

            int id;
            var success = int.TryParse(value.ToString(), out id);
            if (!success)
                return new T();

            var provider = new TreeProvider();
            var node = provider.SelectSingleNode(id);

            return node != null ? mapper.Create<T>(new KenticoTreeNode(node)) : new T();
        }
    }

    public class ComplexBuilder<T> : ComplexBuilder<T, T>
        where T : class, new()
    {
    }
}