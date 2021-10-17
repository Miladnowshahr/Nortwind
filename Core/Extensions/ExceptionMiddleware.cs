using Core.Utility.Exception;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                
            }
        }

        private Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            string message = "Internal Server Error";
            string result = new ErrorDetail
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            }.ToString();

            GetValidationException(context, ex, ref message, ref result);
            GetAuthException(context, ex, ref message, ref result);
            return context.Response.WriteAsync(result, default);
        }

        

        private void GetValidationException(HttpContext context, Exception ex, ref string message, ref string result)
        {
            if (ex.GetType()==typeof(ValidationException))
            {
                var exception = (ValidationException)ex;
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                result = new ErrorDetail
                {
                    Message = exception.Message,
                    StatusCode = context.Response.StatusCode,
                    MessageId=exception.Errors.FirstOrDefault().ErrorCode
                }.ToString();
            }
        }

        private void GetAuthException(HttpContext context,Exception ex,ref string message,ref string result)
        {
            if (ex.GetType()==typeof(AuthException))
            {
                var exception = (AuthException)ex;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                result = new ErrorDetail
                {
                    Message = exception.Message,
                    StatusCode = context.Response.StatusCode,
                    MessageId=exception.MessageId
                }.ToString();
            }
        }
    }
}
