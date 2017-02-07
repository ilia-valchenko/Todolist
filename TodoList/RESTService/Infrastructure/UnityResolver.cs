using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;
using System.Diagnostics;

namespace RESTService.Infrastructure
{
    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;
        private bool disposed;

        public UnityResolver(IUnityContainer container)
        {
            this.container = container;
            disposed = false;
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            var result = new UnityResolver(child);
            return result;
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
                container.Dispose();
                disposed = true;
            }
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException exc)
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
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException exc)
            {
                // write exception into a logfile
                Debug.WriteLine($"Error. Error message: {exc.Message}. StackTrace: {exc.StackTrace}");
                return null;
            }
        }
    }
}