using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Atomic Object")]
    public class AtomicObjectBase : MonoBehaviour, IAtomicObject
    {
        [Title("Data"), PropertySpace, PropertyOrder(100)]
        [ShowInInspector, HideInEditorMode]
        protected internal ISet<string> types = new HashSet<string>();

        [ShowInInspector, HideInEditorMode, PropertyOrder(100)]
        protected internal IDictionary<string, object> references = new Dictionary<string, object>();

        public bool Is(string type)
        {
            return this.types.Contains(type);
        }

        public T Get<T>(string key) where T : class
        {
            if (this.references.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return default;
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            if (this.references.TryGetValue(key, out var value))
            {
                result = value as T;
                return true;
            }

            result = default;
            return false;
        }

        public object Get(string key)
        {
            if (this.references.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public bool TryGet(string key, out object result)
        {
            return this.references.TryGetValue(key, out result);
        }

        public IEnumerable<string> Types()
        {
            return this.types;
        }

        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return this.references;
        }

        public bool AddData(string key, object value)
        {
            return this.references.TryAdd(key, value);
        }

        public void SetData(string key, object value)
        {
            this.references[key] = value;
        }

        public bool RemoveData(string key)
        {
            return this.references.Remove(key);
        }

        public void OverrideData(string key, object value, out object prevValue)
        {
            this.references.TryGetValue(key, out prevValue);
            this.references[key] = value;
        }

        public bool AddType(string type)
        {
            return this.types.Add(type);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            this.types.UnionWith(types);
        }

        public bool RemoveType(string type)
        {
            return this.types.Remove(type);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            foreach (var type in types)
            {
                this.types.Remove(type);
            }
        }
    }
}