namespace WCFService.Model
{
    // Service has information about users: name, surname, account number, email.
    public class User
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public UserAccount Account { get; set; }
    }
}
