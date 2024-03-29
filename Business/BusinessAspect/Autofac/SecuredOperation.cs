﻿using Business.Constant;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.IOC;
using Core.Utility.Exception;
using Core.Utility.Interceptor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.Resolve<IHttpContextAccessor>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthException(Message.AuthenticateDenid,Message.AuthenticateDenidId);
            }

            var roleClaim = _httpContextAccessor.HttpContext.User.ClaimRoles();

            foreach (var role in roleClaim)
            {
                if (_roles.Contains(role))
                {
                    return;
                }
            }
            throw new AuthException(Message.AuthenticateDenid,Message.AuthenticateDenidId);


        }
    }

}
