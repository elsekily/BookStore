using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Entities.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public DateTime TimeIssued { get; set; }
        public User User { get; set; }
        public IList<PurchaseBooks> Books { get; set; }
        public Purchase()
        {
            Books = new List<PurchaseBooks>();
        }
    }
}
