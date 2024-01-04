using System.Collections.Generic;

namespace Atomic.Objects
{
    public interface IAtomicObject
    {
        T Get<T>(string key) where T : class;
        bool TryGet<T>(string key, out T result) where T : class;
        object Get(string key);
        bool TryGet(string key, out object result);
        IEnumerable<KeyValuePair<string, object>> GetAll();
        
        bool Is(string type);
        IEnumerable<string> Types();
    }
}