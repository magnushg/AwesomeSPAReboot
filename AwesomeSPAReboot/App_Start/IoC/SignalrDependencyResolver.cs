using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Ninject;
using Ninject.Activation;

namespace AwesomeSPAReboot.App_Start.IoC
{
    public class SignalrDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel _kernel;

        public SignalrDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
           return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}