using BookStoreLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Core
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        Task<Invoice> GetInvoice(int id);
        Task<IEnumerable<Invoice>> GetInvoices();
        void Remove(Invoice invoice);
    }
}
