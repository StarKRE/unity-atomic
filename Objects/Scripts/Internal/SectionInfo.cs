using System.Collections.Generic;
using System.Reflection;

namespace Atomic.Objects
{
    internal sealed class SectionInfo
    {
        internal readonly IEnumerable<string> types;
        internal readonly IEnumerable<ReferenceInfo> references;
        internal readonly IEnumerable<SectionInfo> children;

        private readonly FieldInfo field;

        internal SectionInfo(
            IEnumerable<string> types,
            IEnumerable<ReferenceInfo> references,
            IEnumerable<SectionInfo> children,
            FieldInfo field
        )
        {
            this.types = types;
            this.references = references;
            this.children = children;
            this.field = field;
        }

        internal object GetValue(object parent)
        {
            return this.field.GetValue(parent);
        }
    }
}