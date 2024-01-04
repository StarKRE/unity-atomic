using System;
using System.Collections.Generic;

namespace Atomic.Objects
{
    public static class Extensions
    {
        public static void AddComponent(this AtomicObjectBase it, object component, bool @override = true)
        {
            Type componentType = component.GetType();

            IEnumerable<string> types = AtomicScanner.ScanTypes(componentType);
            it.AddTypes(types);

            IEnumerable<ReferenceInfo> references = AtomicScanner.ScanReferences(componentType);

            if (@override)
            {
                foreach (var reference in references)
                {
                    string key = reference.id;
                    object value = reference.value(component);

                    if (reference.@override)
                    {
                        it.SetData(key, value);
                    }
                    else
                    {
                        it.AddData(key, value);
                    }
                }
            }
            else
            {
                foreach (var reference in references)
                {
                    string key = reference.id;
                    object value = reference.value(component);
                    it.AddData(key, value);
                }
            }
        }

        public static void RemoveComponent(this AtomicObjectBase it, object component)
        {
            it.RemoveComponent(component.GetType());
        }

        public static void RemoveComponent<T>(this AtomicObjectBase it)
        {
            it.RemoveComponent(typeof(T));
        }

        public static void RemoveComponent(this AtomicObjectBase it, Type componentType)
        {
            IEnumerable<string> types = AtomicScanner.ScanTypes(componentType);
            it.RemoveTypes(types);

            IEnumerable<ReferenceInfo> references = AtomicScanner.ScanReferences(componentType);
            foreach (var reference in references)
            {
                it.RemoveData(reference.id);
            }
        }

        public static void CopyDataFrom(this AtomicObjectBase it, IAtomicObject other, bool @override = true)
        {
            if (@override)
            {
                foreach (var (key, value) in other.GetAll())
                {
                    it.SetData(key, value);
                }
            }
            else
            {
                foreach (var (key, value) in other.GetAll())
                {
                    it.AddData(key, value);
                }
            }
        }

        public static void CopyTypesFrom(this AtomicObjectBase it, IAtomicObject other)
        {
            it.AddTypes(other.Types());
        }
    }
}