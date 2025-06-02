namespace BookShelf.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public string Author { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string BookFilePath { get; set; }
        public string UploadedBy { get; set; }
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
