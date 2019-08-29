using System;
using System.ComponentModel.DataAnnotations;


namespace BusinessLogicLayer.ViewModel
{

    public class UserView
    {
        public int ID { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage="Enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage="Enter Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password mithmach")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter login")]
        public string Login { get; set; }

        public string Captcha { get; set; }

        public string AvatarPath { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BirthdateDate { get; set; }
     
    }
}
    
