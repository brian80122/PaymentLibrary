using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Payment.Models
{
    public class EzPayInvoiceIssueInfo: BasicInfo
    {
        [Required]
        public string RespondType { get; set; }
        [Required]
        public string Version { get; set; }
        [Required]
        public string TimeStamp { get; set; }
        public string TransNum { get; set; }
        [Required]
        public string MerchantOrderNo { get; set; }
        [Required]
        public string Status { get; set; }
        public string CreateStatusTime { get; set; } //YYYY-MM-DD
        [Required]
        public string Category { get; set; }
        [Required]
        public string BuyerName { get; set; }
        public string BuyerUBN { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerEmail { get; set; }
        public string CarrierType { get; set; }
        public string CarrierNum { get; set; }
        public string LoveCode { get; set; }
        [Required]
        public string PrintFlag { get; set; }
        [Required]
        public string TaxType { get; set; }
        [Required]
        public int TaxRate { get; set; }
        public string CustomsClearance { get; set; }
        [Required]
        public int Amt { get; set; }
        public int AmtSales { get; set; }
        public int AmtZero { get; set; }
        public int AmtFree { get; set; }
        [Required]
        public int TaxAmt { get; set; }
        [Required]
        public int TotalAmt { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemCount { get; set; }
        [Required]
        public string ItemUnit { get; set; }
        [Required]
        public string ItemPrice { get; set; }
        [Required]
        public string ItemAmt { get; set; }
        public string ItemTaxType { get; set; }
        public string Comment { get; set; }
    }
}
