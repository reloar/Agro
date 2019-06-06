//using Domain.Models;
//using Microsoft.Extensions.Configuration;
//using Paystack.Net.SDK.Transactions;
//using PayStack.Net;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Payment
//{
//   public class PaystackExtension
//    {
//        private IConfiguration _config;

//        public PaystackExtension(IConfiguration config)
//        {
//            _config = config;
//        }
       
//        //public async Task<SubAccountModel> CreateSubAccount(SubAccountModel model)
//        //{
//        //    var client = HttpConnection
//        //}


//        public async Task<JsonResult> InitializePayment(OrderModel model)
//        {
//            string secretkey = _config.GetValue<string>("PayStack:SecretKey");
//            var paystacktransactionApi = new PaystackTransaction(secretkey);
//            var response = await paystacktransactionApi.InitializeTransaction(model.email, model.amount, model.firstName, model.lastName, "http://localhost:17869/order/callback");

//            if(response.status == true)
//            {
//                return Json(new
//                {
//                    error = false,
//                    result =response
//                }, JsonRequestBehaviour.AllowGet);
//            }
//        }

//    }
//}
