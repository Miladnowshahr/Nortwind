using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Results
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }

        public string MessageId { get; }

        public Result(bool success,string message,string messageId):this(success)
        {
            Message = message;
            MessageId = messageId;
        }
        public Result(bool success)
        {
            Success = success;
        }
    }
}
