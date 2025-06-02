using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;
using BookShelf.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookShelf.Controllers
{
    // Route Book
    [Authorize]
    public class BookController : Controller
    {

        private readonly ApplicationDbContext context;

        public BookController(ApplicationDbContext _context) { context = _context; }

        public IActionResult Overview()
        {
            var books = context.Books.ToList();
            return View(books);
        }

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

        public async Task<IActionResult> Details(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (null == book)
            {
                return NotFound();
            }

            return View(book);
        }
    }

}
