using System;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicValue<T> : IAtomicValue<T>
    {
        public T Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private T value;

        public AtomicValue(T value)
        {
            this.value = value;
        }
    }
}