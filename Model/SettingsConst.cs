namespace Model
{

    public class SettingsConst
    {
        private const string PrefixToGameKeyForMappingProductToGame = "NorthWind-";
        private const string PostfixDelete = "_Deleted";
        private const string lang = "lang";
        private const int expirationDateMonth = 12;
        private const string host = "smtp.gmail.com";
        private const string fromAdress = "Dn260793kev@gmail.com";
        private const string passwordMail = "kolesoxaxol93";
        private const string baseAdress = "http://localhost:8080/payment";

        public static string FromAdress
        {
            get { return fromAdress; }
        }

        public static string BaseAdress
        {
            get { return baseAdress; }
        }

        public static string PasswordMail
        {
            get { return passwordMail; }
        }

        public static int ExpirationDateMonth
        {
            get { return expirationDateMonth; }
        }

        public static string  Host
        {
            get { return host; }
        }

        public static string Prefix
        {
            get { return PrefixToGameKeyForMappingProductToGame; }
        }

        public static string DeletePostfix
        {
            get { return PostfixDelete; }
        }

        public static string LangApi
        {
            get { return lang; }
        }
    }
}