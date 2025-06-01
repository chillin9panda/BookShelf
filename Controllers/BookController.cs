using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;
using BookShelf.Data;

namespace BookShelf.Controllers
{
    // Route Book
    public class BookController : Controller
    {
        public IActionResult Overview()
        {
            var book = new Book()
            {
                Id = 1,
                Title = "Java7",
                Description = "Java openjdk7 instruction",
                Genre = Genre.Programming
            };
            return View(book);
        }

        private readonly ApplicationDbContext context;

        public BookController(ApplicationDbContext _context) { context = _context; }

        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            try
            {

                if (model.BookFile != null && model.BookFile.Length > 0)
                {
                    var fileName = Path.GetFileName(model.BookFile.FileName);
                    var filePath = Path.Combine("wwwroot/BookFiles", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.BookFile.CopyToAsync(stream);
                    }

                    var saveBook = new Book
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Genre = model.Genre,
                        Author = model.Author,
                        PublicationDate = model.PublicationDate,
                        BookFilePath = "/BookFiles/" + fileName
                    };

                    context.Books.Add(saveBook);
                    await context.SaveChangesAsync();

                    return RedirectToAction("Overview");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving book: {e.Message}");
                ModelState.AddModelError("", "Error Saving book");
            }

            return View(model);
        }
    }

}
