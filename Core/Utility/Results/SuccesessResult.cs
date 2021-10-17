using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Results
{
    public class SuccesessResult:Result
    {
        public SuccesessResult(string message,string messageId):base(true,message,messageId)
        {
        }
        public SuccesessResult() : base(true) { }

    }
}
