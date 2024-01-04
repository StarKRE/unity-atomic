using System.Collections.Generic;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Behaviours
{
    public class AtomicBehaviour : AtomicObject
    {
        [Title("Logic"), PropertySpace, PropertyOrder(150)]
        [ShowInInspector, HideInEditorMode]
        private HashSet<ILogic> logicSet;
        
        [ShowInInspector, HideInEditorMode, PropertyOrder(150)]
        private Dictionary<string, ILogic> logicMap;

        private List<IEnable> enables;
        private List<IDisable> disables;
        private List<IUpdate> updates;
        private List<IFixedUpdate> fixedUpdates;
        private List<ILateUpdate> lateUpdates;

        public override void Compose()
        {
            base.Compose();
            this.logicSet = new HashSet<ILogic>();
            this.logicMap = new Dictionary<string, ILogic>();

            this.enables = new List<IEnable>();
            this.disables = new List<IDisable>();

            this.updates = new List<IUpdate>();
            this.fixedUpdates = new List<IFixedUpdate>();
            this.lateUpdates = new List<ILateUpdate>();
        }

        public bool AddLogic(string key, ILogic target)
        {
            if (this.logicMap.TryAdd(key, target))
            {
                return this.AddLogic(target);
            }

            return false;
        }

        public bool AddLogic(ILogic target)
        {
            if (target == null)
            {
                return false;
            }

            if (!this.logicSet.Add(target))
            {
                return false;
            }

            if (target is IEnable enable)
            {
                this.enables.Add(enable);

                if (this.enabled)
                {
                    enable.Enable();
                }
            }

            if (target is IDisable disable)
            {
                this.disables.Add(disable);
            }

            if (target is IUpdate update)
            {
                this.updates.Add(update);
            }

            if (target is IFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (target is ILateUpdate lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
            }

            return true;
        }
        
        public bool RemoveLogic(string key)
        {
            if (this.logicMap.Remove(key, out var target))
            {
                return this.RemoveLogic(target);
            }

            return false;
        }

        public bool RemoveLogic(ILogic target)
        {
            if (target == null)
            {
                return false;
            }
            
            if (!this.logicSet.Remove(target))
            {
                return false;
            }

            if (target is IEnable enable)
            {
                this.enables.Remove(enable);
            }

            if (target is IUpdate tickable)
            {
                this.updates.Remove(tickable);
            }

            if (target is IFixedUpdate fixedTickable)
            {
                this.fixedUpdates.Remove(fixedTickable);
            }

            if (target is ILateUpdate lateTickable)
            {
                this.lateUpdates.Remove(lateTickable);
            }

            if (target is IDisable disable)
            {
                if (this.enabled)
                {
                    disable.Disable();
                }
            }

            return true;
        }

        public void AddLogics(IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                this.AddLogic(target);
            }
        }

        public void RemoveLogics(IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                this.RemoveLogic(target);
            }
        }

        public bool FindLogic<T>(out T result) where T : ILogic
        {
            foreach (var element in this.logicSet)
            {
                if (element is T tElement)
                {
                    result = tElement;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public bool RemoveLogic<T>() where T : ILogic
        {
            foreach (var element in this.logicSet)
            {
                if (element is T)
                {
                    this.RemoveLogic(element);
                    return true;
                }
            }

            return false;
        }

        protected virtual void OnEnable()
        {
            for (int i = 0, count = this.enables.Count; i < count; i++)
            {
                IEnable enable = this.enables[i];
                enable.Enable();
            }
        }

        protected virtual void OnDisable()
        {
            for (int i = 0, count = this.disables.Count; i < count; i++)
            {
                IDisable disable = this.disables[i];
                disable.Disable();
            }
        }

        protected virtual void Update()
        {
            float deltaTime = Time.deltaTime;
            
            for (int i = 0, count = this.updates.Count; i < count; i++)
            {
                IUpdate update = this.updates[i];
                update.OnUpdate(deltaTime);
            }
        }

        protected virtual void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            
            for (int i = 0, count = this.fixedUpdates.Count; i < count; i++)
            {
                IFixedUpdate fixedUpdate = this.fixedUpdates[i];
                fixedUpdate.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0, count = this.lateUpdates.Count; i < count; i++)
            {
                ILateUpdate lateUpdate = this.lateUpdates[i];
                lateUpdate.OnLateUpdate(deltaTime);
            }
        }
    }
}