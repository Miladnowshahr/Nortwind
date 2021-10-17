﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Interceptor
{
    public class MethodInterception:MethodInterceptorBaseAttribute
    {

        protected virtual void  OnBefore(IInvocation invocation) { }
        protected virtual void  OnAfter(IInvocation invocation) { }
        protected virtual void  OnException(IInvocation invocation,Exception e) { }
        protected virtual void  OnSuccessful(IInvocation invocation) { }
        public override void  Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {

                isSuccess = false;
                OnException(invocation,e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccessful(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
