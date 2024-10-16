using ApplicationB.Contracts_B.Order;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DTOsB.OrderBDTOs.ShippingDTO;
using DTOsB.OrderDTO;
using DTOsB.Shared;
using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly int currentUserId;

        public OrderService(IOrderRepository _orderRepository, IMapper _mapper, int _currentUserId)
        {
            orderRepository = _orderRepository;
            mapper = _mapper;
            currentUserId = _currentUserId;

        }
        public async Task<ResultView<AddOrUpdateOrderBDTO>> CreateOrderAsync(AddOrUpdateOrderBDTO orderBDTO)
        {
            var order = mapper.Map<OrderB>(orderBDTO);

            order.CreatedBy = currentUserId;
            order.Created = DateTime.Now;

            await orderRepository.AddAsync(order);
            return ResultView<AddOrUpdateOrderBDTO>.Success(orderBDTO);
        }

        public async Task<ResultView<SelectOrderBDTO>> DeleteOrderAsync(int id)
        {
            var existingOrder = await orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return ResultView<SelectOrderBDTO>.Failure("Shipping not found. Unable to delete.");

            existingOrder.IsDeleted = true;
            existingOrder.UpdatedBy = currentUserId;
            existingOrder.Updated = DateTime.Now;

            await orderRepository.UpdateAsync(existingOrder);
            return ResultView<SelectOrderBDTO>.Success(null);
        }

        public IQueryable<SelectOrderBDTO> GetAllOrdersAsync()
        {
            var orders = orderRepository.GetAll();
            return orders.ProjectTo<SelectOrderBDTO>(mapper.ConfigurationProvider);
        }

        public async Task<ResultView<SelectOrderBDTO>> GetOrderByIdAsync(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
                return ResultView<SelectOrderBDTO>.Failure("Order not found.");

            var orderDto = mapper.Map<SelectOrderBDTO>(order);
            return ResultView<SelectOrderBDTO>.Success(orderDto);
        }

        public async Task<ResultView<AddOrUpdateOrderBDTO>> UpdateOrderAsync(AddOrUpdateOrderBDTO orderBDTO)
        {
            if (orderBDTO == null)
                return ResultView<AddOrUpdateOrderBDTO>.Failure("Order data cannot be null.");

            var existingOrder = await orderRepository.GetByIdAsync(orderBDTO.Id);
            if (existingOrder == null)
                return ResultView<AddOrUpdateOrderBDTO>.Failure("Order not found. Unable to update.");

            mapper.Map(orderBDTO, existingOrder);
            existingOrder.UpdatedBy = currentUserId;
            existingOrder.Updated = DateTime.Now;

            await orderRepository.UpdateAsync(existingOrder);
            return ResultView<AddOrUpdateOrderBDTO>.Success(orderBDTO);
        }
    }
}
