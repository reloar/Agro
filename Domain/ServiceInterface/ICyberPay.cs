using System.Threading.Tasks;
using Domain.Models;

namespace Domain.ServiceInterface
{
   public interface ICyberPay
    {
           Task<(UserModel account, string message)> CreateWallet(UserModel model);
            Task<BankModel[]> GetBanks();
        Task<(PaymentModel model, string message)> Pay(PaymentModel model);
      //  Task<(PaymentModel account, string message)> MakePayment(PaymentModel model);
    }
}