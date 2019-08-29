using System;
using System.ComponentModel.DataAnnotations;


namespace BusinessLogicLayer.ViewModel
{
    public class VisaViewModel
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public double Price { get; set; }

        [Required]
        public string UserName { get; set; }

        [CreditCard]
        public string CardNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateToExpire { get; set; }

        public string CVV2 { get; set; }
    }
}
