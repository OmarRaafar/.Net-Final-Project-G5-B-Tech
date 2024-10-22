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
    public class OrderRepository : GenericRepositoryB<OrderB>, IOrderRepository
    {
        public OrderRepository(BTechDbContext context) : base(context)
        {
        }

        public override async Task<IQueryable<OrderB>> GetAllAsync()
        {
            var orders  = (await base.GetAllAsync()).Where(p => p.IsDeleted == false);
            return orders;
        }
    }
}
