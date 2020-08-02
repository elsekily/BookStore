using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Controllers.Resources
{
    public class SaveBookResource
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public Double Price { get; set; }
        public IList<int> AuthorsIDs { get; set; }
        public SaveBookResource()
        {
            AuthorsIDs = new List<int>();
        }
    }
}
