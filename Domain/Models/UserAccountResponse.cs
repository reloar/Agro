namespace Domain.Models
{
     public class UserAccountResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string WalletCode { get; set; }
    }
}