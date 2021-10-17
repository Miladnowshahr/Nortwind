using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Exception
{
    public class AuthException:AuthenticationException
    {
        public string MessageId { get; }
        public AuthException(string message,string messageId):base(message)
        {
            MessageId = messageId;
        }
    }
}
