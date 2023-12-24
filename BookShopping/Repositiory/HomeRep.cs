using BookShopping.Data;
using BookShopping.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Repositiory
{
    public class HomeRep:IHomeRep
    {
        private readonly ApplicationDbContext context;

        public HomeRep(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Genre> DisplayGenre()
        {
            return context.Genres.ToList();
        }
        public IEnumerable<Book> DisplayBooks(string sterm = "",int genreId=0)
        {
            
            IEnumerable<Book> Books = (from book in context.Books
                         join genre in context.Genres
                         on book.GenreId equals genre.Id
                         where string.IsNullOrWhiteSpace(sterm) || (book != null && book.BookName.StartsWith(sterm))
                                       select new Book {
                             Id=book.Id,
                             BookImage=book.BookImage,
                             AuthorName=book.AuthorName,
                             BookName=book.BookName,
                             Price=book.Price,
                             GenreId=book.GenreId,
                             GenreName=genre.GenreName
                         }
                         ).ToList();
            if (genreId>0)
            {
                Books =  Books.Where(e=>e.GenreId== genreId).ToList();
            }

            return Books;
        }
    }
}
