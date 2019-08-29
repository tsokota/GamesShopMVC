using System.Collections.Generic;

namespace WCFService.Model
{
    // 5.	Do not use DB.  implement fake repository with some data
    public static class FakeDataRepository
    {
        public static List<User> Users { get; set; }
        public static List<UserAccount> UserAccounts { get; set; }
        public static List<TransferTransact> TransferTransacts { get; set; }

        static FakeDataRepository()
        {
            Users = new List<User>();
            UserAccounts = new List<UserAccount>();
            TransferTransacts = new List<TransferTransact>();

            #region Users
            var user1 = new User { UserName = "Yevhenii", UserSurname = "Kolesnik", IdUser = 1, UserEmail = "kolesoxaxol@mail.ru" };
            var user2 = new User { UserName = "Vasya", UserSurname = "Ponamorenko", IdUser = 2, UserEmail = "kolesoxaxol@mail.ru" };
            var user3 = new User { UserName = "Vitalii", UserSurname = "Petrov", IdUser = 3, UserEmail = "kolesoxaxol@mail.ru" };
            var user4 = new User { UserName = "Petr", UserSurname = "Kozlov", IdUser = 4, UserEmail = "kolesoxaxol@mail.ru" };
            var user5 = new User { UserName = "Valentin", UserSurname = "Sidorov", IdUser = 5, UserEmail = "kolesoxaxol@mail.ru" };
            #endregion

            #region Accounts
            var account1 = new UserAccount { OwnerAccount = user1, MoneyAccount = 5000, AccountNumber = "5168742326551706", CVV = "123" };
            var account2 = new UserAccount { OwnerAccount = user2, MoneyAccount = 10000, AccountNumber = "0987654321", CVV = "321" };
            var account3 = new UserAccount { OwnerAccount = user3, MoneyAccount = 15000, AccountNumber = "1029384756", CVV = "333" };
            var account4 = new UserAccount { OwnerAccount = user4, MoneyAccount = 3000, AccountNumber = "5647382910", CVV = "456" };
            var account5 = new UserAccount { OwnerAccount = user5, MoneyAccount = 1000, AccountNumber = "0987612345", CVV = "128" };        
            #endregion

            #region 
            user1.Account = account1;
            user2.Account = account2;
            user3.Account = account3;
            user4.Account = account4;
            user5.Account = account5;

            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
            Users.Add(user4);
            Users.Add(user5);

            UserAccounts.Add(account1);
            UserAccounts.Add(account2);
            UserAccounts.Add(account3);
            UserAccounts.Add(account4);
            UserAccounts.Add(account5);
            #endregion




        }

    }
}
