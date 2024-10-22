using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderBDTOs.OrderItemDTO
{
    public class SelectOrderItemBDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get;set; }
        public int StockQuantity { get; set; }
    }
}
