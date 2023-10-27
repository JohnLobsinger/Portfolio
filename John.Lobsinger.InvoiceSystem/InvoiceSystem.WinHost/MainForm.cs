using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceSystem.WinHost
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateList();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OnInvoiceCreate(object sender, EventArgs e)
        {
            var dlg = new InvoiceForm();
            do
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    _database.Add(dlg.Invoice);
                    UpdateList();
                    return;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show(this, "Please enter a unique invoice name.", "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ValidationException error)
                {
                    var msg = error.ValidationResult.ErrorMessage;
                    MessageBox.Show(this, msg, "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception error)
                {
                    MessageBox.Show(this, error.Message, "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
            } while (true);
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateList()
        {
            _lstInvoices.Items.Clear();
            var movies = from m in _database.GetAll()
                         orderby m.Vendor, m.InvoiceNumber
                         select m;

            _lstInvoices.Items.AddRange(movies.ToArray());
        }
        private readonly IInvoiceDatabase _database = new Data_Storage.SqlInvoiceDatabase(Program.GetConnectionString("InvoiceDatabase"));

        private void OnInvoiceEdit(object sender, EventArgs e)
        {
            var invoice = GetSelectedInvoice();
            if (invoice == null)
                return;

            var dlg = new InvoiceForm();
            dlg.Invoice = invoice;

            do
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    _database.Update(invoice.Id, dlg.Invoice);
                    UpdateList();
                    return;
                }
                catch (Exception error)
                {
                    MessageBox.Show(this, error.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //MessageBox.Show(this, error, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } while (true);
        }

        private Invoice GetSelectedInvoice()
        {
            return _lstInvoices.SelectedItem as Invoice;
        }

        private void OnInvoiceDelete(object sender, EventArgs e)
        {
            var invoice = GetSelectedInvoice();
            if (invoice == null)
                return;

            if (MessageBox.Show(this, $"Are you sure you want to delete invoice #{invoice.InvoiceNumber}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _database.Delete(invoice.Id);
                UpdateList();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
