using Model.Entities;
using DAL;
using Model;

namespace BusinessLogicLayer.Services.UnitOfWorks
{
   public interface IUnitOfWork
    {
        GenericRepository<Comment> CommentRepository { get; set; }
        void Dispose();
        GenericRepository<Game> GameRepository { get; set; }
        GenericRepository<Genre> GenreRepository { get; }
        GenericRepository<OrderDetail> OrderDetailRepository { get; set; }
        GenericRepository<Model.Entities.Order> OrderRepository { get; set; }
        GenericRepository<Platform> PlatformRepository { get; set; }
        GenericRepository<Publisher> PublisherRepository { get; set; }
        GenericRepository<User> UserRepository { get; set; }
        GenericRepository<Role> RoleRepository { get; set; }
        GenericRepository<Language> LanguageRepository { get; set; }
        GenericRepository<GameLang> GameLang { get; set; }
        GenericRepository<BanUser> BanUserRepository { get; set; }

        NorthWindGenericRepository<Product> ProductRepository { get; set; }
        NorthWindGenericRepository<Category> CategoryRepository { get; set; }
        NorthWindGenericRepository<Supplier> SupplierRepository { get; set; }
        NorthWindGenericRepository<DAL.Order> NewOrderRepository { get; set; }
        NorthWindGenericRepository<Shipper> ShipperRepository { get; set; }
        void Save();
        GenericRepository<Model.Reporting.EntityView> ViewRepository { get; set; }
    }
}
