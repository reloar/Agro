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
    public class PaymentController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ICyberPay _payment;
        private readonly ITransactionRepository _storeProduct;
        public PaymentController(IUserRepository userRepo,  ICyberPay payment, ITransactionRepository storeProduct)
        {
            _userRepo = userRepo;
            _payment = payment;
            _storeProduct = storeProduct;
        }

        [HttpPost("initiateTransactiion")]
        public async Task<IActionResult> InitiateTransaction([FromBody] OrderModel model)
        { 
          if (ModelState.IsValid)
            {
                
                var storeProduct = await _storeProduct.SaveProductDetails(model);

                var getProductOwnerAccount = await _userRepo.GetByUserId(storeProduct.userId);
                storeProduct.subaccount = getProductOwnerAccount.walletCodec;
                var makePayment = await _payment.Pay(storeProduct);
                if (makePayment.model != null)
                {
                    return Ok(makePayment.model.authorization_url.ToResponse("Successful"));
                }
                return BadRequest(makePayment.message?.ToResponse(status: HttpStatusCode.BadGateway));
            }
            return BadRequest(ModelState.ToResponse("Invalid Credentials", status: HttpStatusCode.BadRequest));

        }
        
    }
}