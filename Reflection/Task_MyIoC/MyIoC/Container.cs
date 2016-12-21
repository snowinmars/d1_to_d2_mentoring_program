using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	public class Container
	{
		public void AddAssembly(Assembly assembly)
		{ }

		public void AddType(Type type)
		{ }

		public void AddType(Type type, Type baseType)
		{ }

		public object CreateInstance(Type type)
		{
			return null;
		}

		public T CreateInstance<T>()
		{
			return default(T);
		}


		public void Sample()
		{
			var container = new Container();
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
			var customerBLL2 = container.CreateInstance<CustomerBLL>();

			container.AddType(typeof(CustomerBLL));
			container.AddType(typeof(Logger));
			container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
		}
	}
}
