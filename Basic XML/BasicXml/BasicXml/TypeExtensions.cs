using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType &&
                Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }
    }
}
