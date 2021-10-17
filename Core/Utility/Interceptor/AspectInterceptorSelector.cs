using Castle.DynamicProxy;
using Core.Aspect.Autofac.Exception;
using Core.Aspect.Autofac.Logging;
using Core.CrossCuttingConcern.Logging.Log4Net.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Interceptor
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classInterceptor = type.GetCustomAttributes<MethodInterceptorBaseAttribute>(true).ToList();

            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptorBaseAttribute>(true);
            classInterceptor.Add( new LogAspect(typeof(DatabaseLogger)));
            classInterceptor.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));
            classInterceptor.Add(new ExceptionLogAspect(typeof(JsonFileLogger)));
            classInterceptor.AddRange(methodAttributes);

            return classInterceptor.OrderByDescending(x => x.Priority).ToArray();
        }


    }
}