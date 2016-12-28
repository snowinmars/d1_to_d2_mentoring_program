using MyIoCAttributes;
using System;

namespace MyIoC
{
    public class CustomerBll2
    {
        [Import]
        public ICustomerDao CustomerDao { get; set; }

        [Import]
        public Logger Logger { get; set; }

        public void CheckMe()
        {
            Console.WriteLine($"{this.GetType().FullName} working, this.{nameof(this.CustomerDao)} {(object.ReferenceEquals(this.CustomerDao, null) ? "is" : "is not")} null, this.{nameof(this.Logger)} {(object.ReferenceEquals(this.Logger, null) ? "is" : "is not")} null");
        }
    }
}