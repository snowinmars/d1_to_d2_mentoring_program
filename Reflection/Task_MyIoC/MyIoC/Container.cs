using MyIoCAttributes;
using SandS.Algorithm.Extensions.EnumerableExtensionNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyIoC
{
    public class Container
    {
        public Container()
        {
            this.Dependencies = new List<Dependency>(16);
        }

        private IList<Dependency> Dependencies { get; }

        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(type => type.IsClass);

            foreach (var type in types)
            {
                ExportAttribute exportAttribute = type.GetCustomAttributes<ExportAttribute>(inherit: false)
                    .FirstOrDefault(attr => attr.Contract != null);

                Type bindTo = exportAttribute == null ?
                                type : 
                                exportAttribute.Contract;

                this.AddType(type, bindTo);
            }
        }

        public void AddType(Type type)
        {
            this.AddType(type, type);
        }

        public void AddType(Type type, Type baseType) // bad naming, can I change it?
        {
            if (this.Dependencies.Any(d => d.OnRequest == type))
            {
                throw new InvalidOperationException("Type already binded");
            }

            var dep = new Dependency(onRequest: baseType, createThis: type);

            this.Dependencies.Add(dep);
        }

        public object CreateInstance(Type type)
        {
            Dependency dep = this.Dependencies.FirstOrDefault(d => d.OnRequest == type);

            if (dep == default(Dependency))
            {
                throw new TypeInitializationException(type.FullName, null);
            }

            return this.GetInstanceByType(dep.CreateThis);
        }

        public T CreateInstance<T>()
            where T : class
        {
            return this.GetInstanceByType(typeof(T)) as T;
        }

        private object[] DeepInitialize(IEnumerable<ParameterInfo> parameterInfos)
        {
            return parameterInfos.ForEach(p => this.CreateInstance(p.ParameterType)).ToArray();
        }

        private object GetInstanceByType(Type type)
        {
            IEnumerable<ImportConstructorAttribute> attr = type.GetCustomAttributes<ImportConstructorAttribute>(inherit: false);

            return (attr.Any() ?
                this.HandleCtor(type) :
                this.HandleProperties(type));
        }

        private object HandleCtor(Type type)
        {
            ConstructorInfo[] ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (ctors == null ||
                !ctors.Any())
            {
                throw new InvalidOperationException("No ctors definded");
            }

            foreach (var ctor in ctors)
            {
                // I'm trying to resolve at least one ctor, I don't know, does class contains anything else.
                try
                {
                    ParameterInfo[] parameterInfos = ctor.GetParameters();
                    object[] parameterInstances = this.DeepInitialize(parameterInfos); // memory leaks?

                    return ctor.Invoke(parameterInstances);
                }
                catch (TypeInitializationException) { } // this means that I found ctor, but cant resolve it. Who cares, if I can try another one?
            }

            // I can't resolve any ctor, so
            throw new InvalidOperationException("No good ctors was found");
        }

        private object HandleProperties(Type type)
        {
            object obj = Activator.CreateInstance(type);

            IEnumerable<PropertyInfo> props = type.GetProperties()
                                                                .Where(prop => prop.GetCustomAttributes<ImportAttribute>().Any());

            foreach (var prop in props)
            {
                prop.SetValue(obj, this.CreateInstance(prop.PropertyType));
            }

            return obj;
        }
    }
}