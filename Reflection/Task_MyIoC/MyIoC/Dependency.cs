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

        public override string ToString()
        {
            return $"On request {this.OnRequest.Name} create {this.CreateThis.Name}";
        }
    }
}