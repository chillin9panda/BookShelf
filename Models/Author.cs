namespace BookShelf.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public Nationality Nationality { get; set; }

        public List<Book> Books { get; set; } = new();
    }

    public enum Nationality
    { // Used few contries for demo
        American,
        British,
        Ethiopian,
        Japanese,
        Swedes
    }
}
