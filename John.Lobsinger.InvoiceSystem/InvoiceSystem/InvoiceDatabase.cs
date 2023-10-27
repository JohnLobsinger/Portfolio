using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    public abstract class InvoiceDatabase : IInvoiceDatabase
    {
        public Invoice Add(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            var list = GetAllCore();
            foreach (var item in list)
            {
                if (item.InvoiceNumber == invoice.InvoiceNumber)
                    throw new InvalidOperationException("Invoice number mmust be unique");
            }

            return AddCore(invoice);
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than 0");

            DeleteCore(id);
        }

        public Invoice Get(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than 0");

            return GetCore(id);
        }

        public IEnumerable<Invoice> GetAll() => GetAllCore();

        public void Update(int id, Invoice invoice)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than 0");
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            var list = GetAllCore();
            foreach (var item in list)
            {
                if (item.InvoiceNumber == invoice.InvoiceNumber && item.Id != id)
                    throw new InvalidOperationException("Invoice number mmust be unique");
            }

            var existing = GetCore(id);
            if (existing == null)
                throw new InvalidOperationException(nameof(invoice));

            UpdateCore(existing, invoice);
        }

        #region Protected Members

        protected abstract Invoice GetCore(int id);

        protected abstract IEnumerable<Invoice> GetAllCore();

        protected abstract void DeleteCore(int id);

        protected abstract Invoice UpdateCore(Invoice existing, Invoice newItem);

        protected abstract Invoice AddCore(Invoice product);
        #endregion
    }
}
