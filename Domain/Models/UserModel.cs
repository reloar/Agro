namespace Domain.Models
{
    public class UserModel
    {
        public string userId {get;set;}
        public string UserName{get;set;}
        public string LastName{get;set;}
        public string FirstName{get;set;}
        public string Email{get;set;}
        public string roles{get;set;}
        public string Password{get;set;}
        public string PhoneNumber{get;set;}
        public string ConfirmPassword{get;set;}
        public string RegistrationType{get;set;}
        public string bankAccount { get; set; }
        public string subAccountName { get; set; }
        public string bankSortCode { get; set; }
        public string walletCodec { get; set; }
        public string ContactAddress {get;set;}
        public double percentage_charge { get; set; }
        public string settlement_bank { get; set; }
    }
}