using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.TreeNode.Attributes
{
    /// <summary>
    /// Interface for synchronizing data in from the database
    /// </summary>
    public interface ISyncInCollection : INamedSyncIn
    {
        char Delimiter { get; set; }
    }
}