using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class ErrorModel
    {
        public int Status { get; set; }
        public string Path { get; set; }
        public string StrackTrace { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}