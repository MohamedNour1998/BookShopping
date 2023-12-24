using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        [Required]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
