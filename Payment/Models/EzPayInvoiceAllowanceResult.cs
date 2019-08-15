using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class EzPayInvoiceAllowanceResult
    {
        public string MerchantID { get; set; }
        public string AllowanceNo { get; set; }
        public string InvoiceNumber { get; set; }
        public string MerchantOrderNo { get; set; }
        public int AllowanceAmt { get; set; }
        public int RemainAmt { get; set; }
        public string CheckCode { get; set; }

        public bool Validate()
        {
            //尚未實作

            return true;
        }
    }
}
