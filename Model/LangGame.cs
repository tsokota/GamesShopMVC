using Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Model
{
    public class GameLang : IGenericModel
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; } // game ID

        [Key, Column(Order = 2)]
        public int LanguageId { get; set; }

        public string Description { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

        [ForeignKey("Id")]
        public virtual Game Game { get; set; }
    }
}
