using System;
using System.Collections.Generic;
using True.KenticoMapper.Core;
using True.KenticoMapper.Core.Attributes;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode.Mapping
{
    public class SyncInTreeNodeProvider
    {
        private readonly Dictionary<Type, Func<IKenticoTreeNode, ISyncIn, IMapValue>> _mappers = new Dictionary<Type, Func<IKenticoTreeNode, ISyncIn, IMapValue>>
        {
            {typeof(SyncIn), (item, attribute) => new MapTreeNodeValue(item, attribute as INamedSyncIn)},
            {typeof(SyncInDocumentName), (item, attribute) => new MapTreeNodeDocumentName(item)},
            {typeof(SyncInId), (item, attribute) => new MapTreeNodeId(item)},
            {typeof(SyncInParentId), (item, attribute) => new MapTreeNodeParentId(item)},
            {typeof(SyncInRelativeUrl), (item, attribute) => new MapTreeNodeRelativeUrl(item)},
            {typeof(SyncInNodeClassName), (item, attribute) => new MapTreeNodeNodeClassName(item)},
            {typeof(SyncInCollection), (item, attribute) => new MapTreeNodeCollection(item, attribute as ISyncInCollection)}
        };

        public IMapValue GetMapper(IKenticoTreeNode item, ISyncIn attribute)
        {
            Func<IKenticoTreeNode, ISyncIn, IMapValue> mapper;
            if (_mappers.TryGetValue(attribute.GetType(), out mapper))
            {
                return mapper(item, attribute);
            }

            return null;
        }
    }
}