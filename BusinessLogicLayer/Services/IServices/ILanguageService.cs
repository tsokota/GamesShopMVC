using Model;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services.IServices
{
    public interface ILanguageService
    {
        Language Get(string langCode);

        List<Language> GetAllLanguages();

        List<GameLang> GetGameLocalizations(int gameId);
    }
}
