using BookStoreLibrary.Core;
using System.Threading.Tasks;

namespace BookStoreLibrary.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext context;

        public UnitOfWork(BookStoreDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
