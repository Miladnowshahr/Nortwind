using Castle.DynamicProxy;
using Core.CrossCuttingConcern.Validation;
using Core.Utility.Interceptor;
using Core.Utility.Message;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private Type _validator;

        public ValidationAspect(Type validatorType)
        {
            if(typeof(IValidator).IsAssignableFrom(validatorType) is false)
            {
                throw new Exception(CoreMessage.WrongValidationType);
            }
            _validator = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validator);
            var entityType = _validator.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(
                t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }

        }
    }
}
