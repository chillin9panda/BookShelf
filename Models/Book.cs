namespace BookShelf.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateOnly PublicationDate { get; set; }
    }

    public enum Genre
    {
        Programming,
        Fiction,
        NonFiction,
        Fantasy,
        Biography,
        Romance,
        History
    }
}
