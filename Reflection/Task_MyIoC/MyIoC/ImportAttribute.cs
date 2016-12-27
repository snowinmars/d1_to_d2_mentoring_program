using System;

namespace MyIoC
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ImportAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ImportConstructorAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        { }

        public ExportAttribute(Type contract)
        {
            this.Contract = contract;
        }

        public Type Contract { get; private set; }
    }

    [Export]
    public class ContractBll { }

    [Export]
    public class ContractDll { }
}