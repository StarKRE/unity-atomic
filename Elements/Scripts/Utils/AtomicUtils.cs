using System;

namespace Atomic.Elements
{
    internal static class AtomicUtils
    {
        public static void Dispose(ref Action action)
        {
            if (action == null)
            {
                return;
            }

            var delegates = action.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                action -= (Action) @delegate;
            }

            action = null;
        }

        public static void Dispose<T>(ref T @delegate) where T : Delegate
        {
            if (@delegate == null)
            {
                return;
            }

            foreach (Delegate value in @delegate.GetInvocationList())
            {
                @delegate = (T) Delegate.Remove(@delegate, value);
            }

            @delegate = null;
        }

        public static void Dispose<T>(ref Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            var delegates = action.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                action -= (Action<T>) @delegate;
            }

            action = null;
        }
    }
}