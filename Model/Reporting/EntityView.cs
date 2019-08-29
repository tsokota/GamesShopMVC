using System;
namespace Model.Reporting
{
    public  class EntityView :IGenericModel
    {
        // Id View
        public int Id { get; set; }
       
        public string IdEntity { get; set; }
        // platform, genre, game
        public EntityType TypeEntity { get; set; }
        public DateTime DateView { get; set; }
        public EntityView()
        {
            DateView = DateTime.Now;
        }
    }
}
