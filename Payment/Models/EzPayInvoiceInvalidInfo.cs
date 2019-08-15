namespace Payment.Models
{
    public class EzPayInvoiceInvalidInfo : BasicInfo
    {
        public string RespondType { get; set; }
        public string Version { get; set; }
        public string TimeStamp { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvalidReason { get; set; }
    }
}
