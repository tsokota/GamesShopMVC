using BusinessLogicLayer.Filters.PipelinePattern;
using BusinessLogicLayer.ViewModel;
using Model.Entities;
using Model.Filtering;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IGameService : IGenericService<Game>
    {
        IEnumerable<Game> GetAllItems(string langCode);

        Game GetById(int id, string langCode);

        void New(Game game, string langCode, string additionalDescription);

        Game GetByKey(string key, string languageCode);

        int GetGamesCount();

        bool ValidateKey(string key);

        Game GetSideLinks(GameViewModel gameModel);

        void AddLocalizatedDescription(string gameKey, string languageCode, string newDescription);

        IEnumerable<Game> Get(IFilterChain<IQueryable<Game>> filter, string langCode = null);

        IEnumerable<Game> Get(FilterArgs filters, string langCode = null, bool canReturnAnyLanguage = true);
    }
}
