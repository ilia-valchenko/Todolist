using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;
using System.Diagnostics;

namespace RESTService.Infrastructure
{
    public class UnityResolver : IDependencyResolver
    {
        private IUnityContainer Container
        {
            get
            {
                return container;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Unity container");

                container = value;
            }
        }

        public UnityResolver(IUnityContainer container)
        {
            Container = container;
            disposed = false;
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Container.Dispose();
                disposed = true;
            }
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch(ResolutionFailedException exc)
            {
                // write exception into a logfile
                Debug.WriteLine($"Error. Error message: {exc.Message}. StackTrace: {exc.StackTrace}");
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException exc)
            {
                // write exception into a logfile
                Debug.WriteLine($"Error. Error message: {exc.Message}. StackTrace: {exc.StackTrace}");
                return null;
            }
        }

        private IUnityContainer container;
        private bool disposed;
    }
}