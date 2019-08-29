using System;
using BusinessLogicLayer.Filters.PipelinePattern;
using JetBrains.Annotations;
using Model.Entities;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{
    public class GameNameFilter : FilterBase<IQueryable<Game>>
    {
        private readonly string _nameExpression;

        public GameNameFilter(string name)
        {
            if (name == null) _nameExpression = String.Empty;
            else _nameExpression = name.ToLower();
        }

        protected override IQueryable<Game> Process(IQueryable<Game> games)
        {
            return games.Where(a => a.Name.ToLower().Contains(_nameExpression));
        }
    }
}
