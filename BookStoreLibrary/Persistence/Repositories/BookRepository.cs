using BookStoreLibrary.Core;
using BookStoreLibrary.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext context;
        public BookRepository(BookStoreDbContext context)
        {
            this.context = context;
        }
        public async Task<Book> GetBook(int id)
        {
            return await context.Books
                .Include(ba => ba.Authors).ThenInclude(b => b.Author)
                .Include(a => a.User).SingleOrDefaultAsync(a => a.ID == id);
        }
        public async void Add(Book book)
        {
            await context.Books.AddAsync(book);
        }
        public void Remove(Book book)
        {
            context.Books.Remove(book);
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await context.Books
                .Include(a => a.Authors).ThenInclude(ba => ba.Author)
                .Include(b => b.User).ToListAsync();
        }
    }
}
