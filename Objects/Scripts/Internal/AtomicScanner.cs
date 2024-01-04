using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic.Objects
{
    internal static class AtomicScanner
    {
        private static readonly Dictionary<Type, IEnumerable<string>> scannedTypes = new();
        private static readonly Dictionary<Type, IEnumerable<ReferenceInfo>> scannedReferences = new();
        private static readonly Dictionary<Type, IList<SectionInfo>> scannedSections = new();

        internal static IEnumerable<string> ScanTypes(Type target)
        {
            if (scannedTypes.TryGetValue(target, out IEnumerable<string> types))
            {
                return types;
            }

            types = ScanTypesInternal(target);
            scannedTypes.Add(target, types);
            return types;
        }

        internal static IEnumerable<ReferenceInfo> ScanReferences(Type target)
        {
            if (scannedReferences.TryGetValue(target, out IEnumerable<ReferenceInfo> references))
            {
                return references;
            }

            references = ScanReferencesInternal(target);
            scannedReferences.Add(target, references);
            return references;
        }

        internal static IEnumerable<SectionInfo> ScanSections(Type target)
        {
            if (scannedSections.TryGetValue(target, out IList<SectionInfo> sections))
            {
                return sections;
            }

            sections = new List<SectionInfo>();
            ScanSectionsInternal(target, sections);
            scannedSections.Add(target, sections);
            return sections;
        }

        private static IEnumerable<string> ScanTypesInternal(Type target)
        {
            IsAttribute attribute = target.GetCustomAttribute<IsAttribute>();

            if (attribute != null)
            {
                return attribute.types;
            }

            return Array.Empty<string>();
        }

        private static IEnumerable<ReferenceInfo> ScanReferencesInternal(Type target)
        {
            var result = new List<ReferenceInfo>();

            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                GetAttribute attribute = field.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ReferenceInfo(attribute.Id, attribute.Override, field.GetValue);
                result.Add(reference);
            }

            PropertyInfo[] properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];
                GetAttribute attribute = property.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ReferenceInfo(attribute.Id, attribute.Override, property.GetValue);
                result.Add(reference);
            }

            MethodInfo[] methods = ReflectionUtils.GetMethods(target);
            for (int i = 0, count = methods.Length; i < count; i++)
            {
                MethodInfo method = methods[i];
                GetAttribute attribute = method.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ReferenceInfo(attribute.Id, attribute.Override, obj =>
                    method.Invoke(obj, Array.Empty<object>()));

                result.Add(reference);
            }

            return result;
        }


        private static void ScanSectionsInternal(Type parent, IList<SectionInfo> parentList)
        {
            FieldInfo[] fields = ReflectionUtils.GetFields(parent);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];

                if (field.IsDefined(typeof(SectionAttribute)))
                {
                    parentList.Add(ScanSection(field));
                }
            }
        }

        private static SectionInfo ScanSection(FieldInfo sectionField)
        {
            Type sectionType = sectionField.FieldType;
            var types = ScanTypes(sectionType);
            var references = ScanReferences(sectionType);
            var children = ScanSections(sectionType);
            return new SectionInfo(types, references, children, sectionField);
        }
    }
}