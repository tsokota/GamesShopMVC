using BusinessLogicLayer.Filters.PipelinePattern;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{
  
        public class PublisherFilter : FilterBase<IQueryable<Game>>
        {
            private readonly IEnumerable<int> _filterPublisherId;

            public PublisherFilter(IEnumerable<int> listPublisherId)
            {
                _filterPublisherId = listPublisherId;
            }

            protected override IQueryable<Game> Process(IQueryable<Game> games)
            {
                return games.Where(a => _filterPublisherId.Contains(a.Publisher.Id));
            }
        }
   
}
