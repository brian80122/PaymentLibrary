using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class NewebpayQueryTradeInfoResponse : BasicResponse
    {
        public NewebpayQueryTradeInfoResult Result { get; set; }
    }
}
