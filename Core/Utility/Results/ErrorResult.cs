using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Results
{
    public class ErrorResult: Result
    {
        public ErrorResult(string message,string messageId):base(false,message,messageId)
        {}

        public ErrorResult():base(false) { }
    }
}
