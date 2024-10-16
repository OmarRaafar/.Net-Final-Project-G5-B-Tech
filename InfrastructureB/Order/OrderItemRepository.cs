using ApplicationB.Contracts_B.Order;
using DbContextB;
using InfrastructureB.General;
using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB.Order
{
    public class OrderItemRepository : GenericRepositoryB<OrderB>, IOrderItemRepository
    {
        public OrderItemRepository(BTechDbContext context) : base(context)
        {
        }
    }
}
