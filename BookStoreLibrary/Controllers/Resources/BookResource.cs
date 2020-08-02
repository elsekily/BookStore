using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Controllers.Resources
{
    public class BookResource : IdNamePair
    {
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
