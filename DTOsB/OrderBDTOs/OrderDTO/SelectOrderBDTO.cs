using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderDTO
{
    public class SelectOrderBDTO
    {
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value.")]
        public decimal TotalPrice { get; set; }

        public Status CurrentStatus { get; set; } = Status.InCart;

        public string ApplicationUserName { get; set; }

        public decimal ShippingCost { get; set; }

        public string PaymentStatus { get; set; }
    }
}
