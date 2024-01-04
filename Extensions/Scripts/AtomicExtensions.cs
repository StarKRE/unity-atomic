using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class AtomicExtensions
    {
        public static IAtomicValue<T> GetValue<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicValue<T>>(name);
        }
        
        public static IAtomicVariable<T> GetVariable<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicVariable<T>>(name);
        }

        public static IAtomicFunction<T> GetFunction<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicFunction<T>>(name);
        }
        
        public static IAtomicFunction<T1, T2> GetFunction<T1, T2>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicFunction<T1, T2>>(name);
        }
        
        public static IAtomicFunction<T1, T2, T3> GetFunction<T1, T2, T3>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicFunction<T1, T2, T3>>(name);
        }

        public static IAtomicAction GetAction(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicAction>(name);
        }
        
        public static IAtomicAction<T> GetAction<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicAction<T>>(name);
        }
        
        public static IAtomicAction<T1, T2> GetAction<T1, T2>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicAction<T1, T2>>(name);
        }
        
        public static void InvokeAction(this IAtomicObject it, string name)
        {
            it.GetAction(name)?.Invoke();
        }

        public static void InvokeAction<T>(this IAtomicObject it, string name, T args)
        {
            it.GetAction<T>(name)?.Invoke(args);
        }
        
        public static IAtomicObservable<T> GetObservable<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicObservable<T>>(name);
        }
        
        public static IAtomicObservable GetObservable(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicObservable>(name);
        }
    }
}