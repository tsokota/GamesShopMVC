using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Services
{
    public class LanguageService : ILanguageService
    {
      
        private readonly IUnitOfWork _unitOfWork;
      
        public LanguageService(IUnitOfWork uow )
        {
           _unitOfWork = uow;
        }

        public Language Get(string langCode)
        {
            return _unitOfWork.LanguageRepository.Get().FirstOrDefault(x=>x.Code == langCode);
        }

        public List<Language>GetAllLanguages()
        {
            return _unitOfWork.LanguageRepository.Get(a => a.Code != "en").ToList();
        }

        public List<GameLang>GetGameLocalizations(int gameId)
        {
            return _unitOfWork.GameLang.Get(g => g.Description != "" && g.Id == gameId).ToList();
        }
    }
}
