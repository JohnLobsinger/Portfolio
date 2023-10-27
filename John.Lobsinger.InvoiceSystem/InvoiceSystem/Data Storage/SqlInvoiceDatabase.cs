using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Data_Storage
{
    public class SqlInvoiceDatabase : InvoiceDatabase
    {
        public SqlInvoiceDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }
        private readonly string _connectionString;
        protected override Invoice AddCore(Invoice invoice)
        {
            //Open Connection
            using (var conn = OpenConnection())
            {
                //Create Command
                var cmd = new SqlCommand("AddInvoice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vendor", invoice.Vendor);
                cmd.Parameters.AddWithValue("@invoiceNumber", invoice.InvoiceNumber);
                cmd.Parameters.AddWithValue("@invoiceDate", invoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@invoiceTotal", invoice.InvoiceTotal);
                cmd.Parameters.AddWithValue("@paymentTotal", invoice.PaymentTotal);
                cmd.Parameters.AddWithValue("@invoiceDueDate", invoice.InvoiceDueDate);
                cmd.Parameters.AddWithValue("@paymentDate", invoice.PaymentDate);

                object result = cmd.ExecuteScalar();

                invoice.Id = Convert.ToInt32(result);
            }

            return invoice;
        }
        protected override void DeleteCore(int id)
        {
            using (var conn = OpenConnection())
            {
                var cmd = new SqlCommand("DeleteInvoice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
        protected override IEnumerable<Invoice> GetAllCore()
        {
            DataSet ds = new DataSet();
            using (var conn = OpenConnection())
            {
                var cmd = new SqlCommand("GetAllInvoices", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var da = new SqlDataAdapter(cmd);

                da.Fill(ds);
            };

            var table = ds.Tables.OfType<DataTable>().FirstOrDefault();
            if (table != null)
            {
                return table.Rows.OfType<DataRow>().Select(LoadInvoice);
            }
            return Enumerable.Empty<Invoice>();
        }
        protected override Invoice GetCore(int id)
        {
            using (var conn = OpenConnection())
            {
                var cmd = new SqlCommand("GetInvoice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return LoadInvoice(reader);
                    };
                };
            };
            return null;
        }
        protected override Invoice UpdateCore(Invoice existing, Invoice newItem)
        {
            //Open Connection
            using (var conn = OpenConnection())
            {
                //Create Command
                var cmd = new SqlCommand("UpdateInvoice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", existing.Id);
                cmd.Parameters.AddWithValue("@vendor", newItem.Vendor);
                cmd.Parameters.AddWithValue("@invoiceNumber", newItem.InvoiceNumber);
                cmd.Parameters.AddWithValue("@invoiceDate", newItem.InvoiceDate);
                cmd.Parameters.AddWithValue("@invoiceTotal", newItem.InvoiceTotal);
                cmd.Parameters.AddWithValue("@paymentTotal", newItem.PaymentTotal);
                cmd.Parameters.AddWithValue("@invoiceDueDate", newItem.InvoiceDueDate);
                cmd.Parameters.AddWithValue("@paymentDate", newItem.PaymentDate);

                cmd.ExecuteNonQuery();
            }

            return newItem;
        }

        #region Helper Methods
        private SqlConnection OpenConnection()
        {
            //IDisposable
            var conn = new SqlConnection(_connectionString);
            conn.Open();

            return conn;
        }
        private Invoice LoadInvoice(SqlDataReader reader)
        {
            return new Invoice()
            {
                Id              = reader.GetFieldValue<int>("Id"),
                Vendor          = reader.GetFieldValue<string>("Vendor"),
                InvoiceNumber   = reader.GetFieldValue<string>("InvoiceNumber"),
                InvoiceDate     = reader.GetFieldValue<string>("InvoiceDate"),
                InvoiceTotal    = reader.GetFieldValue<Decimal>("InvoiceTotal"),
                PaymentTotal    = reader.GetFieldValue<Decimal>("PaymentTotal"),
                InvoiceDueDate  = reader.GetFieldValue<string>("InvoiceDueDate"),
                PaymentDate     = reader.GetFieldValue<string>("PaymentDate"),
            };
        }
        private Invoice LoadInvoice(DataRow row)
        {
            return new Invoice()
            {
                Id              = row.Field<int>("Id"),
                Vendor          = row.Field<string>("Vendor"),
                InvoiceNumber   = row.Field<string>("InvoiceNumber"),
                InvoiceDate     = row.Field<string>("InvoiceDate"),
                InvoiceTotal    = row.Field<Decimal>("InvoiceTotal"),
                PaymentTotal    = row.Field<Decimal>("PaymentTotal"),
                InvoiceDueDate  = row.Field<string>("InvoiceDueDate"),
                PaymentDate     = row.Field<string>("PaymentDate"),
            };                    
        }                         
        #endregion
    }
}
