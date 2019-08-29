using BusinessLogicLayer.Filters.PipelinePattern;
using JetBrains.Annotations;
using Model.Entities;
using System;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{
    public class DateFilter : FilterBase<IQueryable<Game>>
    {
        private readonly string _date;

        public DateFilter(string date)
        {
            _date = date ?? String.Empty;
        }

        protected override IQueryable<Game> Process(IQueryable<Game> games)
        {
            var filterGames = games;
            DateTime filterDate = DateTime.Now;
            switch (_date)
            {
                case "Last week":
                    filterDate = filterDate.AddDays(-7);
                    break;
                case "Last month":
                    filterDate = filterDate.AddMonths(-1);
                    break;
                case "Last year":
                    filterDate = filterDate.AddYears(-1);
                    break;
                case "2 year":
                    filterDate = filterDate.AddYears(-2);
                    break;
                case "5 year":
                    filterDate = filterDate.AddYears(-5);
                    break;
                case "All time":
                    filterDate = DateTime.MinValue;
                    break;
                default:
                    filterDate = DateTime.MinValue;
                    break;
            }
            return filterGames.Where(a => a.GameProduction >= filterDate);
        }
    }
}
