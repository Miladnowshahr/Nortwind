using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data,string message,string messageId):base(data,false,message,messageId)
        {

        }

        public ErrorDataResult(T data):base(data,false)
        {

        }
        public ErrorDataResult(string message,string messageId) : base(default, false, message,messageId) { }

        public ErrorDataResult() : base(default, false) { }
    }
}
