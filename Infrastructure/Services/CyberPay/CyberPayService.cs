//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Models;
//using Domain.ServiceInterface;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace Infrastructure.Services.CyberPay
//{
//    public class CyberPayService: ICyberPay
//    {
//        private readonly IConfiguration _config;
//        public CyberPayService(IConfiguration config)
//        {
//            _config =config;
//        }

        

//        public async Task<(UserModel account, string message)> CreateWallet(UserModel model){
//            var integrationKey = _config.GetValue<string>("CyberPay:IntegrationKey");
//            var businesscode =_config.GetValue<string>("CyberPay:BusinessCode");
//            var cyberPayUrl = _config.GetValue<string>("CyberPaySubaccount:request");

//            PayStactResponse _response;

//            var payload = new JObject
//            {
//                {"bankAccount",model.bankAccount },
//                {"subAccountName",model.subAccountName },
//                {"bankSortCode",model.bankSortCode }
//            };
//            var uri = cyberPayUrl + businesscode;
//            using (var client =new HttpClient())
//            {
//                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                 client.DefaultRequestHeaders.Add("ApiKey", $"{integrationKey}");
//                var postrequest= await client.PostAsync(uri, new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
//                var response = await postrequest.Content.ReadAsStringAsync();
//                _response =JsonConvert.DeserializeObject<PayStactResponse>(response);

//                if(_response !=null && _response.Data !=null)
//                {
//                    var accountupdate =new UserModel()
//                    {
//                        userId=model.userId,
//                        bankAccount= model.bankAccount,
//                        walletCodec=_response.Data.WalletCode,
//                        ContactAddress=model.ContactAddress,
//                        subAccountName= model.subAccountName,
//                        bankSortCode =model.bankSortCode,
//                        roles=model.roles
//                    };
//                    return (accountupdate, _response?.Message);
//                }
//                return(null, _response?.Message);
//            }
//        }

//        public async Task<BankModel[]> GetBanks()
//        {
//            var client = new HttpClient();
//            JObject result;
//            var transactionUrl = _config.GetValue<string>("CyberPayBanksRequest:banks");
//            var banklink = $"{transactionUrl}/banks";
            
//                string response;
//                using (var res = await client.GetAsync(banklink))
//                {
//                    response = res.Content.ReadAsStringAsync().Result;
//                }

//                result = JObject.Parse(response);
                
//                var data = result["data"].Value<JArray>();
//                BankModel[] bdlst = data.ToObject<BankModel[]>();
//                return bdlst;
//        }

//        public Task<(PaymentModel model, string message)> Pay(PaymentModel model)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}