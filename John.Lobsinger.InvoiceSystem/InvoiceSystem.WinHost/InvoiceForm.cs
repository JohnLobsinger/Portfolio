using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceSystem.WinHost
{
    public partial class InvoiceForm : Form
    {
        public InvoiceForm()
        {
            InitializeComponent();
        }
        public Invoice Invoice { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Invoice != null)
            {
                _txtVendor.Text = Invoice.Vendor;
                _txtInvoiceNumber.Text = Invoice.InvoiceNumber;
                _txtInvoiceDate.Text = Invoice.InvoiceDate.ToString();
                _txtInvoiceTotal.Text = Invoice.InvoiceTotal.ToString();
                _txtPaymentTotal.Text = Invoice.PaymentTotal.ToString();
                _txtInvoiceDueDate.Text = Invoice.InvoiceDueDate.ToString();
                _txtPaymentDate.Text = Invoice.PaymentDate.ToString();
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            var invoice = new Invoice();


            invoice.Vendor = _txtVendor.Text;
            invoice.InvoiceNumber = _txtInvoiceNumber.Text;
            invoice.InvoiceDate = _txtInvoiceDate.Text;
            invoice.InvoiceTotal = Decimal.Parse(_txtInvoiceTotal.Text);
            invoice.PaymentTotal = Decimal.Parse(_txtPaymentTotal.Text);
            invoice.InvoiceDueDate = _txtInvoiceDueDate.Text;
            invoice.PaymentDate = _txtPaymentDate.Text;

            Invoice = invoice;
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to exit?\nThis will leave any changes unsaved.", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
