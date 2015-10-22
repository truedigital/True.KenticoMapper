namespace True.KenticoMapper.Core.Attributes
{
    /// <summary>
    /// Interface for synchronizing data in from the database
    /// </summary>
    public interface INamed
    {
        string Name { get; }
    }    
}