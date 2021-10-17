using Castle.DynamicProxy;
using Core.CrossCuttingConcern.Logging.Log4Net;
using Core.Utility.Interceptor;
using Core.Utility.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Logging
{
    public class LogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerServiceBase))
            {
                throw new Exception(CoreMessage.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private object GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type=invocation.Arguments[i].GetType().Name
                });
            }

            var logDetail = new LogDetail
            {
                Target = invocation.InvocationTarget?.ToString(),
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
