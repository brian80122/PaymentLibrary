using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class EzPayInvoiceIssueResult
    {
        public string MerchantID { get; set; }
        public string InvoiceTransNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public int TotalAmt { get; set; }
        public string InvoiceNumber { get; set; }
        public string RandomNum { get; set; }
        public DateTime CreateTime { get; set; }
        public string CheckCode { get; set; }
        public string BarCode { get; set; }
        public string QRcodeL { get; set; }
        public string QRcodeR { get; set; }

        public string GetCheckCodeInfo()
        {
            var checkCodeArray = new[] {
                                          $"{nameof(InvoiceTransNo)}={InvoiceTransNo}",
                                          $"{nameof(MerchantID)}={MerchantID}",
                                          $"{nameof(MerchantOrderNo)}={MerchantOrderNo}",
                                          $"{nameof(RandomNum)}={RandomNum}",
                                          $"{nameof(TotalAmt)}={TotalAmt}"
                                      };

            return string.Join("&", checkCodeArray);
        }
    }
}
