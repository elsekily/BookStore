using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Entities.Models
{
    public class User : IdentityUser<int>
    {
        public IList<Book> Books { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Purchase> Purchases { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public User()
        {
            Books = new List<Book>();
            Authors = new List<Author>();
            Purchases = new List<Purchase>();
        }
    }
}
