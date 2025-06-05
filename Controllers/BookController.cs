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
                        BookFilePath = "/BookFiles/" + fileName,
                        UploadedBy = User.Identity.Name
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

        public async Task<IActionResult> Download(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (null == book || string.IsNullOrEmpty(book.BookFilePath))
            {
                return NotFound();
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                    book.BookFilePath.TrimStart('/'));
            var contentType = "application/pdf";
            var fileName = Path.GetFileName(book.BookFilePath);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(fileBytes, contentType, fileName);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (null == book)
            {
                return NotFound();
            }

            if (book.UploadedBy != User.Identity.Name)
            {
                return Forbid();
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                    book.BookFilePath.TrimStart('/'));

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return RedirectToAction("Overview");
        }

        public async Task<IActionResult> Modify(int id, BookViewModel model)
        {
            var book = await context.Books.FindAsync(id);
            if (null == book)
            {
                return NotFound();
            }

            if (book.UploadedBy != User.Identity.Name)
            {
                return Forbid();
            }

            var existingBook =
                new BookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Genre = book.Genre,
                    Author = book.Author,
                    PublicationDate = book.PublicationDate,
                    BookFilePath = book.BookFilePath,
                    UploadedBy = User.Identity.Name

                };
            return View(existingBook);
        }
    }

}
