using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Atomic.Objects
{
    public abstract class AtomicObject : AtomicObjectBase
    {
        /// <summary>
        ///     <para>Constructor for atomic object</para>
        /// </summary>
        public virtual void Compose()
        {
#if UNITY_EDITOR
            Profiler.BeginSample("AtomicObjectCompose", this);
#endif
            AtomicObjectInfo objectInfo = AtomicCompiler.CompileObject(this.GetType());
            
            this.AddTypes(objectInfo.types);
            this.AddReferences(this, objectInfo.references);
            this.AddSections(this, objectInfo.sections);
            
#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        private void AddReferences(object source, IEnumerable<ReferenceInfo> definitions)
        {
            foreach (ReferenceInfo definition in definitions)
            {
                string id = definition.id;
                object value = definition.value(source);
                
                if (definition.@override)
                {
                    this.references[id] = value;
                    continue;
                }

                this.references.TryAdd(id, value);
            }
        }
        
        private void AddSections(object parent, IEnumerable<SectionInfo> definitions)
        {
            foreach (var definition in definitions)
            {
                object section = definition.GetValue(parent);
                
                this.AddTypes(definition.types);
                this.AddReferences(section, definition.references);
                this.AddSections(section, definition.children);
            }
        }
        
#if UNITY_EDITOR
        [ContextMenu(nameof(Compose))]
        public void ComposeEditor()
        {
            this.types.Clear();
            this.references.Clear();
            
            this.Compose();
        }
#endif
    }
}