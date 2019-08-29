using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reporting
{
    public  class EntityView
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
