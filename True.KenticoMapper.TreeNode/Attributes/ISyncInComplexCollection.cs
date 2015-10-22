using System;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.TreeNode.Attributes
{
    public interface ISyncInComplexCollection : INamed
    {
        Type Type { get; }
        char Delimiter { get; set; }
    }
}