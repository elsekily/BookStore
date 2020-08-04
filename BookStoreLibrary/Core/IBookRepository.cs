using BookStoreLibrary.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Core
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int id);
        void Add(Book book);
        void Remove(Book book);
    }
}
