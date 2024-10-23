using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderBDTOs.PaymentDTO
{
    public class SelectPaymentBDTO
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = "PayPal";
        public string PaymentStatus { get; set; }  // e.g., "Success", "Failed"
    }
}
