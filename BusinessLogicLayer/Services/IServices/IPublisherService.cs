using Model.Entities;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IPublisherService : IGenericService<Publisher>
    {       
        Publisher GetPublisher(string companyName);
        void SetForGame(string gameKey, string companyName);       
    }
}
