using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Models
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}
