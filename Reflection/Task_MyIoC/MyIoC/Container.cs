using SandS.Algorithm.Extensions.EnumerableExtensionNamespace;
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
	    public Container()
	    {
	        this.Types = new List<Type>();
	    }

	    private IList<Type> Types { get; }

	    public void AddAssembly(Assembly assembly)
	    {
	        assembly.GetTypes().ForEach(this.AddType);
	    }

        private void HandleTypeEntry(Type type)
        {
            var imports = type.GetCustomAttributes(typeof(ImportAttribute), inherit: true);
            var importsCtors = type.GetCustomAttributes(typeof(ImportConstructorAttribute), inherit: true);

            this.Types.Add(type);
        }

        public void AddType(Type type)
	    {
            this.HandleTypeEntry(type);
        }

	    public void AddType(Type type, Type baseType)
	    {
	        this.AddType(type);
            this.AddType(baseType);
	    }

		public object CreateInstance(Type type)
		{
			return null;
		}

		public T CreateInstance<T>()
		{
			return default(T);
		}


		public static void AssemblySample()
		{
			var container = new Container();
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var customerBLL = (CustomerBll)container.CreateInstance(typeof(CustomerBll));
			var customerBLL2 = container.CreateInstance<CustomerBll>();
		}

        public static void DirectSample()
        {
            var container = new Container();

            container.AddType(typeof(CustomerBll));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDao), typeof(ICustomerDao));

            var customerBll = (CustomerBll)container.CreateInstance(typeof(CustomerBll));
            var customerBll2 = container.CreateInstance<CustomerBll>();
        }
    }
}
