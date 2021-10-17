using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _logginEvent;

        public SerializableLogEvent(LoggingEvent logginEvent)
        {
            _logginEvent = logginEvent;
        }

        public object Message => _logginEvent.MessageObject;


    }
}
