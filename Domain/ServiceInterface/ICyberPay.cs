using System.Threading.Tasks;
using Domain.Models;

namespace Domain.ServiceInterface
{
   public interface ICyberPay
    {
           Task<(UserModel account, string message)> CreateWallet(UserModel model);
            Task<BankModel[]> GetBanks();
    }
}