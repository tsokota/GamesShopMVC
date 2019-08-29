using System.ComponentModel.DataAnnotations;
namespace Model
{
    public class Language : IGenericModel
    {
             
        public string Code { get; set; }

        public string Name { get; set; }

        [Key]
        public int Id { get; set; }
      
    }
}
