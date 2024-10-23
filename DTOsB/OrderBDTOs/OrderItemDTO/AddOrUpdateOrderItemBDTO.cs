using ModelsB.Order_B;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderBDTOs.OrderItemDTO
{
    public class AddOrUpdateOrderItemBDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
