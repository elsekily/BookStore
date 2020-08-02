using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public DateTime TimeIssued { get; set; }
        public User User { get; set; }
        public IList<InvoiceBooks> Books { get; set; }
        public Invoice()
        {
            Books = new List<InvoiceBooks>();
        }
    }
}
