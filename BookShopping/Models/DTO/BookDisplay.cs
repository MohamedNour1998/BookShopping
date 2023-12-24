using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Models.DTO
{
    public class BookDisplay
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Sterm { get; set; }="";
        public int GenreId { get; set; }
    }
}
