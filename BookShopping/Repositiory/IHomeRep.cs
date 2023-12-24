using BookShopping.Models;
using System.Collections.Generic;

namespace BookShopping
{
    public interface IHomeRep
    {
        IEnumerable<Book> DisplayBooks(string sTerm = "", int genreId = 0);
        IEnumerable<Genre> DisplayGenre();
    }
}