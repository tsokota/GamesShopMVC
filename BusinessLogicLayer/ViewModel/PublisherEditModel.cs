using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessLogicLayer.ViewModel
{
   public class PublisherEditModel
    {
      
      [HiddenInput(DisplayValue = false)]
      public int Id { get; set; }

      [Display(Name = "Key of company")]
      [DataType(DataType.Text)]
      [Required]
      public string CompanyName { get; set; }

      [Display(Name = "Description")]
      [DataType(DataType.MultilineText)]
      public string Description { get; set; }

      [Display(Name = "Home page")]
      [DataType(DataType.Url)]
      public string HomePage { get; set; }      
    }
}
