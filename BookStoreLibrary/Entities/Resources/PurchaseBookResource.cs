using System.ComponentModel.DataAnnotations;

namespace BookStoreLibrary.Entities.Resources
{
    public class PurchaseBookResource
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        [Range(0, int.MaxValue)]
        public double BookPrice { get; set; }
        [Range(1, int.MaxValue)]
        public int NumberOfItems { get; set; }
        [Range(0, int.MaxValue)]
        public double BookDiscount { get; set; }
        [Range(0, int.MaxValue)]
        public double TotalPrice { get; set; }
    }
}
