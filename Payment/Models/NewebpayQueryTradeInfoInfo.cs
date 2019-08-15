using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class NewebpayQueryTradeInfoInfo: BasicInfo
    {
        public string MerchantID { get; set; }
        public string Version { get; set; }
        public string RespondType { get; set; }
        public string CheckValue { get; set; }
        public string TimeStamp { get; set; }
        public string MerchantOrderNo { get; set; }
        public int Amt { get; set; }
    }
}
