using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Controllers.Resources
{
    public class BookSaveResource
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public IList<int> Authors { get; set; }
        public BookSaveResource()
        {
            Authors = new List<int>();
        }
    }
}
