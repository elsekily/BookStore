using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public User User { get; set; }
        public IList<AuthorBooks> Books { get; set; }
        public Author()
        {
            Books = new List<AuthorBooks>();
        }
    }
}
