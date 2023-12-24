using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string BookName { get; set; }

        [Required]
        [MaxLength(40)]
        public string AuthorName { get; set; }
        public string? BookImage { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<CartDetail>cartDetails { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public string GenreName  { get; set; }
    }
}
