﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Logging.Log4Net.Logger
{
   public class JsonFileLogger:LoggerServiceBase
    {
        public JsonFileLogger():base("JsonFileLogger")
        {

        }
    }
}
