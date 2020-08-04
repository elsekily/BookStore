using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Entities.Resources
{
    public class AuthorSaveResource
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public IList<int> Books { get; set; }
        public AuthorSaveResource()
        {
            Books = new List<int>();
        }
    }
}
