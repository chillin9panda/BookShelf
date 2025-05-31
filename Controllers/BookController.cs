using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;

namespace BookShelf.Controllers
{
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
    }

}
