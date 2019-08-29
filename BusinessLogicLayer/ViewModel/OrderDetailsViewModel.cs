using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessLogicLayer.ViewModel
{
    public class OrderDetailsViewModel
    {
         
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [DataType(DataType.Text)]
        [Required]
        public string ProductName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [Display(Name = "Products count")]
        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Total cost")]
        [Required]
        public double Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double Discount { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
    }
}
