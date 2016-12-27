using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIoC;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            container.AddType(typeof(CustomerBll));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDao), typeof(ICustomerDao));

            var customerBll = (CustomerBll)container.CreateInstance(typeof(CustomerBll));
            customerBll.CheckMe();

            var customerBll2 = container.CreateInstance<CustomerBll2>();
            customerBll2.CheckMe();
        }
    }
}
