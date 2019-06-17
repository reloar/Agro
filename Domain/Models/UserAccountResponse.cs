using Newtonsoft.Json;

namespace Domain.Models
{
     public class PayStactResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string WalletCode { get; set; }
        public string subaccount_code { get; set; }
        public string account_name { get; set; }
        public string authorization_url { get; set; }
        public string access_code { get; set; }
        public string reference { get; set; }
    }

    public class TransactionInitializeRequest 
    {
        public string Reference { get; set; }

        [JsonProperty("amount")]
        public int AmountInKobo { get; set; }

        public string Email { get; set; }

        public string Plan { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("subaccount")]
        public string SubAccount { get; set; }

        [JsonProperty("transaction_charge")]
        public int TransactionCharge { get; set; }

        public string Bearer { get; set; }
    }
}