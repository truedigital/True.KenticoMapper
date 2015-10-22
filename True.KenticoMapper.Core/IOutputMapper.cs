namespace True.KenticoMapper.Core
{
    public interface IOutputMapper
    {
        void GetProperties<T>(T source, IIndexer target);
    }
}