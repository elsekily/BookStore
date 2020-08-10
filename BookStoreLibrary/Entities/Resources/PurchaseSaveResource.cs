using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Entities.Resources
{
    public class PurchaseSaveResource
    {
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        [Range(1, int.MaxValue)]
        public double Discount { get; set; }
        [Range(0, int.MaxValue)]
        public double TotalPrice { get; set; }
        public IList<PurchaseBookResource> Books { get; set; }
        public PurchaseSaveResource()
        {
            Books = new List<PurchaseBookResource>();
        }
    }
}
