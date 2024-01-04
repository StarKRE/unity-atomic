using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicAction : IAtomicAction
    {
        private Action action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action action)
        {
            this.action = action;
        }

        public void Compose(Action action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke()
        {
            this.action?.Invoke();
        }
    }

    [Serializable]
    public class AtomicAction<T> : IAtomicAction<T>
    {
        private Action<T> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T> action)
        {
            this.action = action;
        }
        
        public void Compose(Action<T> action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke(T direction)
        {
            this.action?.Invoke(direction);
        }
    }
    
    [Serializable]
    public class AtomicAction<T1, T2> : IAtomicAction<T1, T2>
    {
        private Action<T1, T2> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T1, T2> action)
        {
            this.action = action;
        }
        
        public void Compose(Action<T1, T2> action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke(T1 args1, T2 args2)
        {
            this.action?.Invoke(args1, args2);
        }
    }
    
    [Serializable]
    public class AtomicAction<T1, T2, T3> : IAtomicAction<T1, T2, T3>
    {
        private Action<T1, T2, T3> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T1, T2, T3> action)
        {
            this.action = action;
        }
        
        public void Compose(Action<T1, T2, T3> action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.action?.Invoke(args1, args2, args3);
        }
    }
}