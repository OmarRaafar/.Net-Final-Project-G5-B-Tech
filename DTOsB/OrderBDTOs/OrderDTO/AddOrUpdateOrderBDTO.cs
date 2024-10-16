using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Order_B;

namespace DTOsB.OrderDTO
{
    public class AddOrUpdateOrderBDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value.")]
        public decimal TotalPrice { get; set; }

        public Status CurrentStatus { get; set; } = Status.InCart;

        public string ApplicationUserId { get; set; }

        public int ShippingId { get; set; }

        public int PaymentId { get; set; }

    }
}
