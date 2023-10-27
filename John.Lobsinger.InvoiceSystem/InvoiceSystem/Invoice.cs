using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceSystem
{
    public class Invoice
    {
        [Required]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Vendor
        {
            get { return _vendor ?? ""; }
            set { _vendor = value?.Trim(); }
        }
        [Required(AllowEmptyStrings = false)]
        public string InvoiceNumber
        {
            get { return _invoiceNumber ?? ""; }
            set { _invoiceNumber = value?.Trim(); }
        }
        [Required(AllowEmptyStrings = false)]
        public string InvoiceDate
        {
            get { return _invoiceDate ?? ""; }
            set { _invoiceDate = value?.Trim(); }
        }
        [Range(0, Double.MaxValue)]
        public decimal InvoiceTotal { get; set; }
        [Range(0, Double.MaxValue)]
        public decimal PaymentTotal { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string InvoiceDueDate
        {
            get { return _invoiceDueDate ?? ""; }
            set { _invoiceDueDate = value?.Trim(); }
        }
        public string PaymentDate
        {
            get { return _paymentDate ?? ""; }
            set { _paymentDate = value?.Trim(); }
        }

        public Invoice Copy() => new Invoice()
        {
            Id              = Id,
            Vendor          = Vendor,
            InvoiceNumber   = InvoiceNumber,
            InvoiceDate     = InvoiceDate,
            InvoiceTotal    = InvoiceTotal,
            PaymentTotal    = PaymentTotal,
            InvoiceDueDate  = InvoiceDueDate,
            PaymentDate     = PaymentDate,
        };
        public void CopyFrom(Invoice source)
        {
            Id = source.Id;
            Vendor = source.Vendor;
            InvoiceNumber = source.InvoiceNumber;
            InvoiceDate = source.InvoiceDate;
            InvoiceTotal = source.InvoiceTotal;
            PaymentTotal = source.PaymentTotal;
            InvoiceDueDate = source.InvoiceDueDate;
            PaymentDate = source.PaymentDate;
        }

        public override string ToString() => $"{Vendor} | {InvoiceNumber} | {InvoiceDate} | {InvoiceTotal} | {PaymentTotal} | {InvoiceDueDate} | {PaymentDate}";

        #region Private members

        private string _invoiceNumber;
        private string _invoiceDate;
        private string _invoiceDueDate;
        private string _paymentDate;
        private string _vendor;

        #endregion
    }
}
