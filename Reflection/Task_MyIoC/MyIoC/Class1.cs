using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
    public class User
    {
        
    }

	[ImportConstructor]
	public class CustomerBll
	{
	    private readonly ICustomerDao dao;
	    private readonly Logger logger;

	    public CustomerBll(User user)
        { }

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
