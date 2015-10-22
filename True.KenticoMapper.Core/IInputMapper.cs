namespace True.KenticoMapper.Core
{
    public interface IInputMapper
    {
        void SetProperties<T>(IIndexer source, T target) where T : class;
    }
}