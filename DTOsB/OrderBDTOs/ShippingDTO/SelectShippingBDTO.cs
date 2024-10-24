using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderBDTOs.ShippingDTO
{
    public class SelectShippingBDTO
    {
        public string ShippingAddress { get; set; }

        public string ShippingMethod { get; set; }  // e.g., "Standard", "Express"

        public decimal ShippingCost { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

        public decimal OrderTotalPrice { get; set; }
    }
}
