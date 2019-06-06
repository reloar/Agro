using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Models;
using Domain.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ICyberPay _cyberpay;
        private IUserRepository _userRepo;
        public UserController(ICyberPay cyberPay, IUserRepository userRepository)
        {
            _cyberpay = cyberPay;
            _userRepo = userRepository;
        }
        [HttpGet("bank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<BankModel[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<BankModel>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBank()
        {
            var banks = await _cyberpay.GetBanks();
            return Ok(banks.ToResponse("Successful"));
           
        }

        [HttpPost("updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserModel model)
        {   
            if(ModelState.IsValid)
            {
                var createwallet = await _cyberpay.CreateWallet(model);
                if(createwallet.account !=null)
                {
                    await _userRepo.UpdateUserProfile(createwallet.account);
                    return Ok(createwallet.account.ToResponse("Succesful"));
                }
                
               return BadRequest(createwallet.message?.ToResponse(status: HttpStatusCode.BadGateway));
            }
            return BadRequest(ModelState.ToResponse("Invalid Credentials", status: HttpStatusCode.BadRequest));
    
        }
    }
}