using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ErrorDetail
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string MessageId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this,new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }
    }
}
