using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Domain;
using Domain.Interface;
using Domain.Models;
using Domain.Utilities;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly INotificationUtility _notification;
        private readonly DbContext _context;
        private readonly RoleManager<Role> _roleManager;
        public UserRepository(IConfiguration config, INotificationUtility notification, 
        UserManager<User> userManager, DbContext context, RoleManager<Role> roleManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _notification = notification;
        }
        public async Task<UserModel> FindUserbyEmail(string email)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);

            var response = new UserModel().Assign(user);
            response.userId = user.Id;
            response.Email = user.Email;
            response.RegistrationType = user.UserType;
            return response;
        }

        public async Task<UserModel> GetByUserId(string UserId)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == UserId);

            var response = new UserModel().Assign(user);
            response.walletCodec = user.WalletCode;
            return response;
        }

        public string getRegistrationType()
        {
            var usertypes = _config.GetValue<string>("User:Types");
            return usertypes;
        }

        public async Task<(bool Status, string Message)> RegisterUser(UserModel model)
        {
            var user = new User().Assign(model);
            user.UserName = model.Email;
            user.UserType = model.RegistrationType;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName ?? ""),
                    new Claim(ClaimTypes.Email, user.UserName ?? ""),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.Surname, model.LastName ?? ""),
                    new Claim(ClaimTypes.Role, model.RegistrationType)
                };
                var claimResult = await _userManager.AddClaimsAsync(user, claims);
                if (claimResult.Succeeded)
                {

                    var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, Constants.DefaultPhone);


                    _notification.SendMail(new MailModel
                    {
                        To = model.Email,
                        Subject = "AGRO Commodities Exchange",
                        Content = $"Dear {model.FirstName}Your account has been created successfully as a {model.RegistrationType}<br />" +
                      $" Your Login Details: <br/> Username : {model.Email} <br/> <br /> click the link below" +
                      $"to login <a href='{HtmlEncoder.Default.Encode(Constants.AGROX_URl + "/Account/Login")}'>CLick here</a> or" +
                      $"<a href='{HtmlEncoder.Default.Encode(code)}'>CLick here</a> ",

                    });
                    return (true, "The user has been created with the right claims");
                }

            }
            return (false, "Unable to create user");
        }

        public async Task<UserModel> UpdateUserProfile(UserModel model)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(r => r.Id == model.userId);
      
            
            if(user != null)
            {
                user.WalletCode = model.walletCodec;
                user.BankAccountNumber = model.bankAccount;
                user.Accountname = model.subAccountName;
                user.ContactAddress=model.ContactAddress;         
                
                await _context.SaveChangesAsync();
            }
            var returnedModel = new UserModel().Assign(user);
            returnedModel.subAccountName = user.Accountname;
            returnedModel.roles = model.roles;
            return returnedModel;

        }
    }
}