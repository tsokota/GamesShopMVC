using Model.Entities;
using System.ComponentModel.DataAnnotations;


namespace Model.MetaEntities
{
    class MetaOrderDetail
    {
        [Key]
        public int Id { get; set; }


        public string ProuctId { get; set; }

        [Display(Name = "OrderPrice", ResourceType = typeof(ModelRes))]
        public double Price { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(ModelRes))]
        public int Quantity { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(ModelRes))]
        public double Discount { get; set; }

        public virtual Order Order { get; set; }

        [Display(Name = "OrderType", ResourceType = typeof(ModelRes))]
        public OrderType OrderType { get; set; }

        public virtual Game Product { get; set; }

    }
}
