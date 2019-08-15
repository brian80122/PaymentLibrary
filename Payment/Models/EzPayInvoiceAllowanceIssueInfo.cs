using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class EzPayInvoiceAllowanceIssueInfo : BasicInfo
    {
        public string RespondType { get; set; }
        public string Version { get; set; }
        public string TimeStamp { get; set; }
        public string InvoiceNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public string ItemUnit { get; set; }
        public int ItemPrice { get; set; }
        public int ItemAmt { get; set; }
        public int? TaxTypeForMixed { get; set; }
        public int ItemTaxAmt { get; set; }
        public int TotalAmt { get; set; }
        public string BuyerEmail { get; set; }
        public string Status { get; set; }
    }
}
