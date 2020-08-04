namespace BookStoreLibrary.Entities.Models
{
    public class AuthorBooks
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
    }
}
