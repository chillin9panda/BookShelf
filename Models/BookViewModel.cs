using Microsoft.AspNetCore.Http;
using BookShelf.Models;

public class BookViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Genre Genre { get; set; }
    public string Author { get; set; }
    public DateOnly PublicationDate { get; set; }
    public IFormFile BookFile { get; set; }
    public string BookFilePath { get; set; }
    public string UploadedBy { get; set; }
}
