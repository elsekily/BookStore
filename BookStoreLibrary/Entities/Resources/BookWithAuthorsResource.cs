using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Entities.Resources
{
    public class BookWithAuthorsResource : IdNamePair
    {
        public string AddedBy { get; set; }
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public ICollection<IdNamePair> Authors { get; set; }
        public BookWithAuthorsResource()
        {
            Authors = new Collection<IdNamePair>();
        }
    }
}
