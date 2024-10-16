using DTOsB.OrderBDTOs.ShippingDTO;
using DTOsB.Product;
using DTOsB.Shared;

namespace ApplicationB.Services_B.Order
{
    public interface IShippingService
    {
        Task<ResultView<AddOrUpdateShippingBDTO>> CreateShippingAsync(AddOrUpdateShippingBDTO shippingBDTO);
        Task<ResultView<AddOrUpdateShippingBDTO>> UpdateShippingAsync(AddOrUpdateShippingBDTO shippingBDTO);
        Task<ResultView<SelectShippingBDTO>> DeleteShippingAsync(int id);
        Task<ResultView<SelectShippingBDTO>> GetShippingByIdAsync(int id);
        public IQueryable<SelectShippingBDTO> GetAllShippingsAsync();
    }
}