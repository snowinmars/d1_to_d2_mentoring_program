using MyIoCAttributes;
using System;

namespace MyIoC
{
    [ImportConstructor]
    public class CustomerBll
    {
        private readonly ICustomerDao dao;
        private readonly Logger logger;

        public CustomerBll(ICustomerDao dao, Logger logger)
        {
            this.dao = dao;
            this.logger = logger;
        }

        public void CheckMe()
        {
            Console.WriteLine($"{this.GetType().FullName} working, this.{nameof(this.dao)} {(object.ReferenceEquals(this.dao, null) ? "is" : "is not")} null, this.{nameof(this.logger)} {(object.ReferenceEquals(this.logger, null) ? "is" : "is not")} null");
        }
    }
}