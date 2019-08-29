using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.ViewModel
{
    public class LoginView
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password")]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}
