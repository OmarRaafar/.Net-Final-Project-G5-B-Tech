using DTOsB.OrderBDTOs.PaymentDTO;
using DTOsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Order
{
    public class PaymentService : IPaymentService
    {
        public Task<ResultView<AddOrUpdatePaymentBDTO>> CreatePaymentAsync(AddOrUpdatePaymentBDTO paymentBDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<SelectPaymentBDTO>> DeletePaymentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<IEnumerable<SelectPaymentBDTO>>> GetAllPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<SelectPaymentBDTO>> GetPaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<AddOrUpdatePaymentBDTO>> UpdatePaymentAsync(AddOrUpdatePaymentBDTO paymentBDTO)
        {
            throw new NotImplementedException();
        }
    }
}
