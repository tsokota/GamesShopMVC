using System;
using System.Collections.Generic;
using System.Globalization;
using Model.Entities;

namespace BusinessLogicLayer.ViewModel
{
    public class OrderHistoryViewModel
    {
        private DateTime startDate;
        private DateTime endDate;

        public List<Order> OrderList { get; set; }

        public string StartDateToString
        {
            get
            {
                return startDate.ToString("yyyy-MM-dd");

            }
            set
            {
                startDate = DateTime.ParseExact(value, "yyyy-MM-dd", new DateTimeFormatInfo());
            }
        }

        public string EndDateToString
        {
            get
            {
                return endDate.ToString("yyyy-MM-dd");
            }
            set
            {
                endDate = DateTime.ParseExact(value, "yyyy-MM-dd", new DateTimeFormatInfo());
            }
        }

        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
    }
}
