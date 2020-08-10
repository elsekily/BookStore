namespace BookStoreLibrary.Entities.Models
{
    public class PurchaseBooks
    {
        public int ID { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }
        public int PurchaseID { get; set; }
        public Purchase Purchase { get; set; }

        public int NumberOfItems { get; set; }
        public double BookPrice { get; set; }
        public double BookDiscount { get; set; }
        public double TotalPrice { get; set; }
    }
}
