using BookStoreLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreLibrary.Core
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
        void Add(Author author);
        void Remove(Author author);
    }
}
