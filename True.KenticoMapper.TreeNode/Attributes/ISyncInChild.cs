using System;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.TreeNode.Attributes
{
    public interface ISyncInChild : INamed
    {
        Type Type { get; }
    }
}