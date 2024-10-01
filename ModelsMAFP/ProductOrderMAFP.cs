using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsMAFP
{
    public class ProductOrderMAFP
    {
        public int Id { get; set; }
        [ForeignKey("ProductMAFP")]
        public int ProductMAFPId { get; set; } 
        public ProductMAFP Product { get; set; }
        [ForeignKey("OrderMAFP")]
        public int OrderMAFPID { get; set; }  
        public OrderMAFP Order { get; set; }
        public int Quantity { get; set; }
    }
}
