using System;

namespace True.KenticoMapper.Core
{
    public interface IIndexer
    {
        object this[string index] { get; set; }
    }

    public class Indexer : IIndexer
    {
        private readonly Func<string, object> _getter;
        private readonly Action<string, object> _setter;

        public object this[string index]
        {
            get { return _getter(index); }
            set { _setter(index, value); }
        }

        public Indexer(Func<string, object> getter, Action<string, object> setter)
        {
            _getter = getter;
            _setter = setter;
        }
    }
}
