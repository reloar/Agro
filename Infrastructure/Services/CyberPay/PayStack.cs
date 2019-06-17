using Domain.Models;
using Domain.ServiceInterface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CyberPay
{
    public class PayStackService : ICyberPay
    {
        private readonly IConfiguration _config;
        public PayStackService(IConfiguration config)
        {
            _config = config;
        }

        

        public async Task<(UserModel account, string message)> CreateWallet(UserModel model)
        {
            var charge = _config.GetValue<string>("PayStack:Charge");
            var secretKey = _config.GetValue<string>("PayStack:SecretKey");
            var transactionUrl = _config.GetValue<string>("PayStack:Url");
            
            PayStactResponse _response;

            var payload = new JObject
            {
                {"account_number",model.bankAccount },
                {"business_name",model.subAccountName },
                {"settlement_bank",model.bankSortCode },
                {"percentage_charge", charge  }
            };
            var uri = $"{transactionUrl}subaccount";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authorization", $"Bearer {secretKey}");//("Bearer sk_test_d8f105888681f7efcc9821ce9d1109499bb19417");
              //  client.DefaultRequestHeaders.Add("ApiKey", $"{integrationKey}");
                var postrequest = await client.PostAsync(uri, new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
                var response = await postrequest.Content.ReadAsStringAsync();
                _response = JsonConvert.DeserializeObject<PayStactResponse>(response);

                if (_response != null && _response.Data != null)
                {
                    var accountupdate = new UserModel()
                    {
                        userId = model.userId,
                        bankAccount = model.bankAccount,
                        walletCodec =  _response.Data.subaccount_code,//_response.Data.WalletCode,
                        ContactAddress = model.ContactAddress,
                        subAccountName = model.subAccountName,
                        bankSortCode = model.bankSortCode,
                        //model.bankSortCode,
                        
                        roles = model.roles
                    };
                    return (accountupdate, _response?.Message);
                }
                return (null, _response?.Message);
            }
        }

        public async Task<BankModel[]> GetBanks()
        {
            var client = new HttpClient();
            JObject result;
            var transactionUrl = _config.GetValue<string>("PayStack:Url");
            var banklink = $"{transactionUrl}/bank";

            string response;
            using (var res = await client.GetAsync(banklink))
            {
                response = res.Content.ReadAsStringAsync().Result;
            }

            result = JObject.Parse(response);

            var data = result["data"].Value<JArray>();
            BankModel[] bdlst = data.ToObject<BankModel[]>();
            return bdlst;
        }

        public async Task<(PaymentModel model, string message)> Pay(PaymentModel model)
        {
            var secretKey = _config.GetValue<string>("PayStack:SecretKey");
            var transactionUrl = _config.GetValue<string>("PayStack:Url");

            var email = _config.GetValue<string>("PayStack:email");
            model.amount = model.amount * 100;
            model.email = email;

            var payload = JsonConvert.SerializeObject(model);
            PayStactResponse _response;


            var uri = $"{transactionUrl}transaction/initialize";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authorization", $"Bearer {secretKey}");//("Bearer sk_test_d8f105888681f7efcc9821ce9d1109499bb19417");
                                                                                         //  client.DefaultRequestHeaders.Add("ApiKey", $"{integrationKey}");
                var postrequest = await client.PostAsync(uri, new StringContent(payload, Encoding.UTF8, "application/json"));
                var response = await postrequest.Content.ReadAsStringAsync();
                _response = JsonConvert.DeserializeObject<PayStactResponse>(response);

                if (_response != null && _response.Data != null)
                {
                    var paymentupdate = new PaymentModel()
                    {
                        userId = model.userId,
                        amount = model.amount/100,
                        subaccount = model.subaccount,
                        authorization_url=  _response.Data.authorization_url,
                        accesscode = _response.Data.access_code,
                        reference =_response.Data.reference,
                        email =model.email
                    };
                    return (paymentupdate, _response?.Message);
                }
                return (null, _response?.Message);
            }
        }
    }
}
