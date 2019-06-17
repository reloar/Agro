using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ITransactionRepository
    {
        Task<PaymentModel> SaveProductDetails(OrderModel model);
       // Task<PaymentModel> SaveDeliveryDetails(OrderModel model);
    }
}
