using BookStoreLibrary.Core;
using BookStoreLibrary.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreDbContext context;
        public AuthorRepository(BookStoreDbContext context)
        {
            this.context = context;
        }
        public async void Add(Author author)
        {
            await context.Authors.AddAsync(author);
        }
        public async Task<Author> GetAuthor(int id)
        {
            return await context.Authors
                .Include(a => a.Books).ThenInclude(ba => ba.Book)
                .Include(a => a.User).SingleOrDefaultAsync(a => a.ID == id);
        }
        public void Remove(Author author)
        {
            context.Remove(author);
        }
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await context.Authors
                .Include(a => a.Books).ThenInclude(ba => ba.Book)
                .Include(a => a.User).ToListAsync();
        }
    }
}
