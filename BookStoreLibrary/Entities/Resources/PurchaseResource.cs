using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Entities.Resources
{
    public class PurchaseResource
    {
        public int ID { get; set; }
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public double Discount { get; set; }
        [Range(0, int.MaxValue)]
        public double TotalPrice { get; set; }
        public DateTime TimeIssued { get; set; }
        public string AddedBy { get; set; }
        public IList<PurchaseBookResource> Books { get; set; }
        public PurchaseResource()
        {
            Books = new List<PurchaseBookResource>();
        }
    }
}
