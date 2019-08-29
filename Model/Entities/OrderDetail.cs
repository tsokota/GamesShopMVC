using Model.MetaEntities;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [MetadataType(typeof(MetaOrderDetail))]
    public class OrderDetail : IGenericModel
    {
        public int Id { get; set; }
    
        public string ProuctId { get; set; }
   
        public double Price { get; set; }
   
        public int Quantity { get; set; }
   
        public double Discount { get; set; }

        public virtual Order Order { get; set; }

        public OrderType OrderType { get; set; }

        public virtual Game Product { get; set; }
        
    }
}
