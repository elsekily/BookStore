using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Entities.Resources
{
    public class InvoiceResource
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
        public IList<InvoiceBookResource> Books { get; set; }
        public InvoiceResource()
        {
            Books = new List<InvoiceBookResource>();
        }
    }
}
