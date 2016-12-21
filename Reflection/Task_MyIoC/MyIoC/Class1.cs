using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	[ImportConstructor]
	public class CustomerBll
	{
		public CustomerBll(ICustomerDao dao, Logger logger)
		{ }
	}

	public class CustomerBll2
	{
		[Import]
		public ICustomerDao CustomerDao { get; set; }
		[Import]
		public Logger Logger { get; set; }
	}
}
