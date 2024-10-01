﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsMAFP
{
    public class ShippingMAFP
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string ShippingAddress { get; set; }
        [MaxLength(50)]
        public string ShippingMethod { get; set; }  // e.g., "Standard", "Express"
        [Column(TypeName = "money")]
        public decimal ShippingCost { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        [ForeignKey("OrderMAFP")]
        public int OrderId { get; set; }  // Foreign key for Order
        public OrderMAFP Order { get; set; }
    }
}
