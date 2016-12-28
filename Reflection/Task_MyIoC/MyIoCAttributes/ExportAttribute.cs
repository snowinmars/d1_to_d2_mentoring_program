using System;

namespace MyIoCAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        {
            this.Contract = null;
        }

        public ExportAttribute(Type contract)
        {
            this.Contract = contract;
        }

        public Type Contract { get; private set; }
    }
}