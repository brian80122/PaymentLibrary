using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class EzPayInvoiceInvalidResult
    {
        public string MerchantID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public string CheckCode { get; set; }
    }
}
