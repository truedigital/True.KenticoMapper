using System;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.TreeNode.Attributes
{
    public interface ISyncInChildren : INamed
    {
        Type Type { get; }
    }
}