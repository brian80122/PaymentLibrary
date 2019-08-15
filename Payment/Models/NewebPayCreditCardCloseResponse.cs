using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class NewebPayCreditCardCloseResponse : BasicResponse
    {
        public NewebPayCreditCardCloseResult Result { get; set; }
    }

    public class NewebPayCreditCardCloseResult
    {
        public string MerchantID { get; set; }
        public int Amt { get; set; }
        public string TradeNo { get; set; }
        public string MerchantOrderNo { get; set; }
    }
}
