using System;
using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Builders
{
    public class ComplexCollectionBuilder<T> : ComplexCollectionBuilder<T, T>
        where T : class, new()
    {
    }

    public class ComplexCollectionBuilder<T, TResult> : IComplexCollectionBuilder
        where T : class, TResult, new()
        where TResult : class
    {
        public object Build(IKenticoTreeNode source, ISyncInComplexCollection attribute, ITreeNodeMapper mapper)
        {
            if (string.IsNullOrEmpty(attribute.Name))
            {
                return mapper.Create<T>(source);
                //if no matching attribute name is found, attempt to map from the object itself
            }

            var value = source[attribute.Name];
            if (value == null)
                return Enumerable.Empty<TResult>();

            var components = value.ToString().Split(new[] { attribute.Delimiter}, StringSplitOptions.RemoveEmptyEntries);

            var provider = new TreeProvider();
            var nodes = new List<IKenticoTreeNode>();
            foreach (var component in components)
            {
                int id;
                var success = int.TryParse(component, out id);
                if (!success)
                    continue;

                if (id == 0)
                    continue;

                var node = provider.SelectSingleNode(id);
                if (node != null)
                    nodes.Add(new KenticoTreeNode(node));
            }

            return nodes
                .Select(mapper.Create<T>)
                .Where(i => i != null);
        }
    }
}