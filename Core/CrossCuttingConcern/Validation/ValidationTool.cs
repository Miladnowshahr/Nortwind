using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Validation
{
    public static class ValidationTool
    {
        public static void Validate<T>(IValidator validator, T entity) where T:class,new()
        {
            var result = validator.Validate(new ValidationContext<T>(entity));

            if (result.IsValid is false)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
