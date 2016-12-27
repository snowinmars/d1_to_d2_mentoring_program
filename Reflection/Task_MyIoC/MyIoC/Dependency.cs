using System;

namespace MyIoC
{
    public class Dependency
    {
        public Dependency(Type onRequest, Type createThis)
        {
            this.OnRequest = onRequest;
            this.CreateThis = createThis;
        }

        public Type OnRequest { get; }
        public Type CreateThis { get; }
    }
}