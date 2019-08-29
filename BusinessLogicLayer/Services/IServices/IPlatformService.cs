using Model.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services.IServices
{
    public interface IPlatformService : IGenericService<Platform>
    {
        void SetForGame(string gameKey, IEnumerable<string> platformNames);
    }
}
