namespace WCFService.Model
{
    // 	Service has information about accounts: account owner, amount of money.
    public class UserAccount
    {
        public string AccountNumber { get; set; }
        public User OwnerAccount { get; set; }
        public string CVV { get; set; }
        public decimal MoneyAccount {get;set;} 
    }
}
