using System.Collections.Generic;

namespace BookStoreLibrary.Controllers.Resources
{
    public class AuthorWithBooksResource : IdNamePair
    {
        public string AddedBy { get; set; }
        public IList<BookResource> Books { get; set; }
        public AuthorWithBooksResource()
        {
            Books = new List<BookResource>();
        }
    }
}
