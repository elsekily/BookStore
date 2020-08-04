using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Entities.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }
        public IList<AuthorBooks> Authors { get; set; }
        public IList<InvoiceBooks> Invoices { get; set; }
        public Book()
        {
            Authors = new List<AuthorBooks>();
            Invoices = new List<InvoiceBooks>();
        }
    }
}
