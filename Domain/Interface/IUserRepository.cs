using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserRepository
    {
        Task<(bool Status, string Message)> RegisterUser(UserModel model);
        string getRegistrationType();
         Task<UserModel> FindUserbyEmail(string email);
        Task<UserModel> GetByUserId(string UserId);
    }
}