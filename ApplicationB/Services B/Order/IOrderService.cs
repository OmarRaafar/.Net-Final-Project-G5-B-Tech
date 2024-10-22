using DTOsB.OrderBDTOs.PaymentDTO;
using DTOsB.OrderDTO;
using DTOsB.Shared;

namespace ApplicationB.Services_B.Order
{
    public interface IOrderService
    {
        Task<ResultView<AddOrUpdateOrderBDTO>> CreateOrderAsync(AddOrUpdateOrderBDTO orderBDTO);
        Task<ResultView<AddOrUpdateOrderBDTO>> UpdateOrderAsync(AddOrUpdateOrderBDTO orderBDTO);
        Task<ResultView<SelectOrderBDTO>> DeleteOrderAsync(int id);
        Task<SelectOrderBDTO> GetOrderByIdAsync(int id);
        Task<IEnumerable<SelectOrderBDTO>> GetAllOrdersAsync();
        


    }
}