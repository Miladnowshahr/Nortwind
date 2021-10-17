using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message,string messageId):base(data,true,message,messageId)
        {
        }
        public SuccessDataResult(T data) : base(data, true)
        { }

        public SuccessDataResult(string message,string messageId) : base(default, true, message,messageId)
        { }

        public SuccessDataResult() : base(default, true) { }
    }
}
