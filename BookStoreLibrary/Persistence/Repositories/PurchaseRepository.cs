using BookStoreLibrary.Core;
using BookStoreLibrary.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Persistence.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly BookStoreDbContext context;
        public PurchaseRepository(BookStoreDbContext context)
        {
            this.context = context;
        }
        public async void Add(Purchase purchase)
        {
            foreach (var purchaseBook in purchase.Books)
            {
                var bookfromDb = await context.Books.SingleOrDefaultAsync(b => b.ID == purchaseBook.BookID);
                bookfromDb.Quantity += purchaseBook.NumberOfItems;
            }
            await context.Purchases.AddAsync(purchase);
        }
        public async Task<Purchase> GetPurchase(int id)
        {
            return await context.Purchases.Include(i => i.User)
                .Include(i => i.Books).ThenInclude(ib => ib.Book)
                .SingleOrDefaultAsync(a => a.ID == id);
        }
        public async Task<IEnumerable<Purchase>> GetPurchases()
        {
            return await context.Purchases
                .Include(i => i.User)
                .Include(i => i.Books).ThenInclude(ib => ib.Book).ToListAsync();
        }
        public async void Remove(Purchase purchase)
        {
            foreach (var purchaseBook in purchase.Books)
            {
                var bookfromDb = await context.Books.SingleOrDefaultAsync(b => b.ID == purchaseBook.BookID);
                bookfromDb.Quantity -= purchaseBook.NumberOfItems;
            }
            context.Purchases.Remove(purchase);
        }
    }
}
