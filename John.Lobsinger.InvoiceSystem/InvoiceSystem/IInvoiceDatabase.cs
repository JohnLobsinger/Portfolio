using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    public interface IInvoiceDatabase
    {
        Invoice Add(Invoice invoice);
        void Delete(int id);
        Invoice Get(int id);
        IEnumerable<Invoice> GetAll();
        void Update(int id, Invoice invoice);
    }
}
