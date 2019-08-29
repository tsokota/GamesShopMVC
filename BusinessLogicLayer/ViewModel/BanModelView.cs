using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.ViewModel
{
    public class BanModelView
    {
        public BanUser UserBan { get; set; }

        public Dictionary<string, int> BanTime { get; set; }

        public int BanVariant { get; set; }

        public BanModelView()
        {
            BanTime = new Dictionary<string, int> {{"1 hour", 1}, {"1 day", 2}, {"1 month", 3}, {"1 forever", 4}};
        }
    }
}
