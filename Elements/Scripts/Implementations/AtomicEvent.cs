using System;
using Sirenix.OdinInspector;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicEvent : IAtomicEvent, IDisposable
    {
        private Action onEvent;

        public void Subscribe(Action action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke()
        {
            this.onEvent?.Invoke();
        }

        public void Dispose()
        {
            AtomicUtils.Dispose(ref this.onEvent);
        }
    }

    [Serializable]
    public class AtomicEvent<T> : IAtomicEvent<T>, IDisposable
    {
        private Action<T> onEvent;
        
        public void Subscribe(Action<T> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke(T direction)
        {
            this.onEvent?.Invoke(direction);
        }

        public void Dispose()
        {
            AtomicUtils.Dispose(ref this.onEvent);
        }
    }
    
    [Serializable]
    public class AtomicEvent<T1, T2> : IAtomicEvent<T1, T2>, IDisposable
    {
        private Action<T1, T2> onEvent;
        
        public void Subscribe(Action<T1, T2> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action<T1, T2> action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke(T1 args1, T2 args2)
        {
            this.onEvent?.Invoke(args1, args2);
        }

        public void Dispose()
        {
            AtomicUtils.Dispose(ref this.onEvent);
        }
    }
    
    [Serializable]
    public class AtomicEvent<T1, T2, T3> : IAtomicEvent<T1, T2, T3>, IDisposable
    {
        private Action<T1, T2, T3> onEvent;
        
        public void Subscribe(Action<T1, T2, T3> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action<T1, T2, T3> action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.onEvent?.Invoke(args1, args2, args3);
        }

        public void Dispose()
        {
            AtomicUtils.Dispose(ref this.onEvent);
        }
    }
}