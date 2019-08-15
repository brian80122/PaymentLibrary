using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class BasicResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public bool IsSucceed
        {
            get
            {
                return !string.IsNullOrEmpty(Status) && Status == "SUCCESS";
            }
        }
    }
}
