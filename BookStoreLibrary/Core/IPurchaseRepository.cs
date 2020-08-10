using BookStoreLibrary.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Core
{
    public interface IPurchaseRepository
    {
        void Add(Purchase purchase);
        Task<Purchase> GetPurchase(int id);
        Task<IEnumerable<Purchase>> GetPurchases();
        void Remove(Purchase purchase);
    }
}
