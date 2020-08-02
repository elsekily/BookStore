using BookStoreLibrary.Core;
using BookStoreLibrary.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly BookStoreDbContext context;
        public InvoiceRepository(BookStoreDbContext context)
        {
            this.context = context;
        }
        public async Task<Invoice> GetInvoice(int id)
        {
            return await context.Invoices.Include(i => i.User)
                .Include(i => i.Books).ThenInclude(ib => ib.Book)
                .SingleOrDefaultAsync(a => a.ID == id);
        }
        public async void Add(Invoice invoice)
        {
            foreach (var invoiceBook in invoice.Books)
            {
                var bookfromDb = await context.Books.SingleOrDefaultAsync(b => b.ID == invoiceBook.BookID);
                bookfromDb.Quantity -= invoiceBook.NumberOfItems;
                if (bookfromDb.Quantity < 0)
                    throw new Exception();
            }
            await context.Invoices.AddAsync(invoice);
        }
        public async void Remove(Invoice invoice)
        {
            foreach (var invoiceBook in invoice.Books)
            {
                var bookfromDb = await context.Books.SingleOrDefaultAsync(b => b.ID == invoiceBook.BookID);
                bookfromDb.Quantity += invoiceBook.NumberOfItems;
            }
            context.Invoices.Remove(invoice);
        }
        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await context.Invoices
                .Include(i => i.User)
                .Include(i => i.Books).ThenInclude(ib => ib.Book).ToListAsync();
        }
    }
}
