using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Objects
{
    public sealed class AtomicProxy : MonoBehaviour, IAtomicObject
    {
        [SerializeField]
        public AtomicObject source;

        public T Get<T>(string key) where T : class
        {
            return this.source.Get<T>(key);
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            return this.source.TryGet(key, out result);
        }

        public object Get(string key)
        {
            return this.source.Get(key);
        }

        public bool TryGet(string key, out object result)
        {
            return this.source.TryGet(key, out result);
        }

        public IEnumerable<string> Types()
        {
            return this.source.Types();
        }

        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return this.source.GetAll();
        }

        public bool Is(string type)
        {
            return this.source.Is(type);
        }
    }
}