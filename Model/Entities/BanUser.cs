using Model.MetaEntities;
using System;
using System.ComponentModel.DataAnnotations;


namespace Model.Entities
{
    [MetadataType(typeof(MetaBanUser))]
    public class BanUser:IGenericModel
    {
    
        public int Id { get; set; }

        public DateTime BeginBan { get; set; }

        public DateTime LastBan { get; set; }

        public string ReasonBan { get; set; }

        public int IdUser { get; set; }

        public int IdComment { get; set; }
    }
}
