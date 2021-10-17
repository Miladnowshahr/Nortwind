using Castle.DynamicProxy;
using Core.CrossCuttingConcern.Caching;
using Core.IOC;
using Core.Utility.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Cache
{
    public class CacheRemoveAspect:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cache;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cache = ServiceTool.Resolve<ICacheManager>();
        }

        protected override void OnSuccessful(IInvocation invocation)
        {
            _cache.RemoveByPattern(_pattern);
        }
    }
}
