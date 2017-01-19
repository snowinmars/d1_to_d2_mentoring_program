using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.Common;

namespace BasicXml
{
    public static class TypeExtensions
    {
        public static DateTime RandomDateTime()
        {
            DateTime start = new DateTime(1800, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(Constants.Random.Next(range));
        }

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
