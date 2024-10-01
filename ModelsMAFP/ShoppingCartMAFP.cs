using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsMAFP
{
    //The ShoppingCart model is used to manage a user's current session, including products they've added to their cart.
    public class ShoppingCartMAFP
    {
        public int Id { get; set; }
        [Column(TypeName = "money")]
        public decimal DiscountAmount { get; set; } 
        public string? CouponCode { get; set; } 
        [ForeignKey("CustomerMAFP")]
        public int CustomerMAFPId { get; set; } 
        public CustomerMAFP Customer { get; set; }
        public List<CartItemMAFP> CartItems { get; set; } 
    }
}
