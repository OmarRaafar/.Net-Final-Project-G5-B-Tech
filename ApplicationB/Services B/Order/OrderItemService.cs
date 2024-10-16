using ApplicationB.Contracts_B.Order;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DTOsB.OrderBDTOs.OrderItemDTO;
using DTOsB.OrderBDTOs.ShippingDTO;
using DTOsB.Shared;
using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Order
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;
        private readonly int currentUserId;

        public OrderItemService(IOrderItemRepository _orderItemRepository, IMapper _mapper, int _currentUserId)
        {
            orderItemRepository = _orderItemRepository;
            mapper = _mapper;
            currentUserId = _currentUserId;

        }
        public async Task<ResultView<AddOrUpdateOrderItemBDTO>> CreateOrderItemAsync(AddOrUpdateOrderItemBDTO orderItemBDTO)
        {
            var orderItem = mapper.Map<OrderB>(orderItemBDTO);

            orderItem.CreatedBy = currentUserId;
            orderItem.Created = DateTime.Now;

            await orderItemRepository.AddAsync(orderItem);
            return ResultView<AddOrUpdateOrderItemBDTO>.Success(orderItemBDTO);
        }

        public async Task<ResultView<SelectOrderItemBDTO>> DeleteOrderItemAsync(int id)
        {
            var existingOrderItem = await orderItemRepository.GetByIdAsync(id);
            if (existingOrderItem == null)
                return ResultView<SelectOrderItemBDTO>.Failure("Order not found. Unable to delete.");

            existingOrderItem.IsDeleted = true;
            existingOrderItem.UpdatedBy = currentUserId;
            existingOrderItem.Updated = DateTime.Now;

            await orderItemRepository.UpdateAsync(existingOrderItem);
            return ResultView<SelectOrderItemBDTO>.Success(null);
        }

        public IQueryable<SelectOrderItemBDTO> GetAllOrderItemsAsync()
        {
            var OrderItems = orderItemRepository.GetAll();
            return OrderItems.ProjectTo<SelectOrderItemBDTO>(mapper.ConfigurationProvider);
        }

        public async Task<ResultView<SelectOrderItemBDTO>> GetOrderItemByIdAsync(int id)
        {
            var order = await orderItemRepository.GetByIdAsync(id);
            if (order == null)
                return ResultView<SelectOrderItemBDTO>.Failure("Order not found.");

            var orderDto = mapper.Map<SelectOrderItemBDTO>(order);
            return ResultView<SelectOrderItemBDTO>.Success(orderDto);
        }

        public async Task<ResultView<AddOrUpdateOrderItemBDTO>> UpdateOrderItemAsync(AddOrUpdateOrderItemBDTO orderItemBDTO)
        {
            if (orderItemBDTO == null)
                return ResultView<AddOrUpdateOrderItemBDTO>.Failure("Order item data cannot be null.");

            var existingOrderItem = await orderItemRepository.GetByIdAsync(orderItemBDTO.Id);
            if (existingOrderItem == null)
                return ResultView<AddOrUpdateOrderItemBDTO>.Failure("Order item not found. Unable to update.");

            mapper.Map(orderItemBDTO, existingOrderItem);
            existingOrderItem.UpdatedBy = currentUserId;
            existingOrderItem.Updated = DateTime.Now;

            await orderItemRepository.UpdateAsync(existingOrderItem);
            return ResultView<AddOrUpdateOrderItemBDTO>.Success(orderItemBDTO);
        }
    }
}
