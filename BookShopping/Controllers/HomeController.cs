using BookShopping.Models;
using BookShopping.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRep homeRep;

        public HomeController(ILogger<HomeController> logger,IHomeRep homeRep)
        {
            _logger = logger;
            this.homeRep = homeRep;
        }

        public IActionResult Index(string sterm="",int genreId = 0)
        {
           
            IEnumerable<Book> Books = homeRep.DisplayBooks(sterm, genreId);
            IEnumerable<Genre> genres = homeRep.DisplayGenre();
            BookDisplay bookDisplay = new BookDisplay {
                Books = Books,
                Genres = genres,
                Sterm=sterm,
                GenreId=genreId
            };
            return View(bookDisplay);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
