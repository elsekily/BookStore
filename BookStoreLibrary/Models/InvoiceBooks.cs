namespace BookStoreLibrary.Models
{
    public class InvoiceBooks
    {
        public int ID { get; set; }
        
        public int BookID { get; set; }
        public Book Book { get; set; }
        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
        
        public int NumberOfItems { get; set; }
        public double BookPrice { get; set; }
        public double BookDiscount { get; set; }
        public double TotalPrice { get; set; }
    }
}
