using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsB.OrderBDTOs.OrderItemDTO;

namespace DTOsB.OrderDTO
{
    public class SelectOrderBDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public Status CurrentStatus { get; set; } = Status.InCart;

        public string ApplicationUserName { get; set; }

        public decimal ShippingCost { get; set; }

        public string PaymentStatus { get; set; }

        public IEnumerable<SelectOrderItemBDTO> OrderItems { get; set; }
    }
}
//public enum Status
//{
//    [Display(Name = "In Cart")]
//    InCart,
//    [Display(Name = "Pending")]
//    Pending,
//    [Display(Name = "Shipped")]
//    Shipped,
//    [Display(Name = "Delivered")]
//    Delivered,
//    [Display(Name = "Cancelled")]
//    Cancelled
//}