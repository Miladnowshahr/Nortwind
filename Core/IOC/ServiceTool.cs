using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IOC
{
    public static class ServiceTool
    {
        private static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection serviceProvider)
        {
            ServiceProvider = serviceProvider.BuildServiceProvider();
            return serviceProvider;
        }

        public static T Resolve<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
