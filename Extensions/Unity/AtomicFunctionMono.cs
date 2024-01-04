using Atomic.Elements;
using UnityEngine;

namespace Atomic.Extensions
{
    public abstract class AtomicFunctionMono<T1> : MonoBehaviour, IAtomicFunction<T1>
    {
        public abstract T1 Invoke();
    }
    
    public abstract class AtomicFunctionMono<T1, T2> : MonoBehaviour, IAtomicFunction<T1, T2>
    {
        public abstract T2 Invoke(T1 args);
    }
    
    public abstract class AtomicFunctionMono<T1, T2, T3> : MonoBehaviour, IAtomicFunction<T1, T2, T3>
    {
        public abstract T3 Invoke(T1 args1, T2 args2);
    }
}