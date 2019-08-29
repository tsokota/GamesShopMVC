using Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;



namespace DAL
{
    public class UniqueKeyAttribute : ValidationAttribute
    {
        readonly GameStoreContext _db;
        UniqueKeyAttribute(GameStoreContext db)
        {
           _db = db;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
        
                var owner = validationContext.ObjectInstance as Game;
                if (owner == null) return new ValidationResult("Model is empty");
                
              

                var key = _db.Games.FirstOrDefault(u => u.Key == (string)value);

                return key == null ? ValidationResult.Success : new ValidationResult("Key already exists");
         
        }
    }
}
