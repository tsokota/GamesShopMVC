using System;

namespace Model.Payments.Args
{
    public class VisaPayArgs : PayArgs
    {
        public string CardHoldersName { get; set; }
        public string CarNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV2_CVC2 { get; set; }
    }
}
