using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   
   
    public class PaymentModel:Model
    {
        public string reference { get; set; }
        public string email { get; set; }
        public decimal  amount { get; set; }
        public string callback_url { get; set; }
        public string subaccount { get; set; }
        public string userId { get; set; }
        public string authorization_url { get; set; }
        public string accesscode { get; set; }
    }
   }
