using Model;
using Model.Reporting;
using System;
using Model.Entities;
using DAL;


namespace BusinessLogicLayer.Services.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreContext _context;
        private readonly NORTHWNDContext _nwndContext;

        public UnitOfWork(GameStoreContext context, NORTHWNDContext nwndContext)
        {
            _context = context;
            _nwndContext = nwndContext;
            AutomapperConfig.ConfigurationMapper();
        }

        private GenericRepository<Game> _gameRepository;
        private GenericRepository<Genre> _genreRepository;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<EntityView> _viewRepository;
        private GenericRepository<Platform> _platfromRepository;
        private GenericRepository<Publisher> _publisherRepository;
        private GenericRepository<Model.Entities.Order> _orderRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<Language> _languageRepository;
        private GenericRepository<GameLang> _gameLangrepository;
        private GenericRepository<BanUser> _banUserRepository;

        // new repositories for work with NORTHWND
        private NorthWindGenericRepository<Product> _productRepository;
        private NorthWindGenericRepository<Category> _categoryRepository;
        private NorthWindGenericRepository<Supplier> _supplierRepository;
        private NorthWindGenericRepository<DAL.Order> _neworderRepository;
        private NorthWindGenericRepository<Shipper> _shipperRepository;


        #region Property My Repositories
        public GenericRepository<BanUser> BanUserRepository
        {
            get { return _banUserRepository ?? (_banUserRepository = new GenericRepository<BanUser>(_context)); }
            set
            {
                _banUserRepository = value;
            }
        }


        public GenericRepository<GameLang> GameLang
        {
            get { return _gameLangrepository ?? (_gameLangrepository = new GenericRepository<GameLang>(_context)); }
            set
            {
                _gameLangrepository = value;
            }
        }


        public GenericRepository<Language> LanguageRepository
        {
            get { return _languageRepository ?? (_languageRepository = new GenericRepository<Language>(_context)); }
            set
            {
                _languageRepository = value;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new GenericRepository<User>(_context)); }
            set
            {
                _userRepository = value;
            }
        }

        public GenericRepository<Game> GameRepository
        {
            get { return _gameRepository ?? (_gameRepository = new GenericRepository<Game>(_context)); }
            set
            {
                _gameRepository = value;
            }
        }

        public GenericRepository<Genre> GenreRepository
        {
            get { return _genreRepository ?? (_genreRepository = new GenericRepository<Genre>(_context)); }
        }

        public GenericRepository<Comment> CommentRepository
        {
            get { return _commentRepository ?? (_commentRepository = new GenericRepository<Comment>(_context)); }
            set
            {
                _commentRepository = value;
            }

        }

        public GenericRepository<EntityView> ViewRepository
        {
            get { return _viewRepository ?? (_viewRepository = new GenericRepository<EntityView>(_context)); }
            set
            {
                _viewRepository = value;
            }
        }

        public GenericRepository<Platform> PlatformRepository
        {
            get { return _platfromRepository ?? (_platfromRepository = new GenericRepository<Platform>(_context)); }
            set
            {
                _platfromRepository = value;
            }
        }

        public GenericRepository<Publisher> PublisherRepository
        {
            get { return _publisherRepository ?? (_publisherRepository = new GenericRepository<Publisher>(_context)); }
            set
            {
                _publisherRepository = value;
            }
        }

        public GenericRepository<Model.Entities.Order> OrderRepository
        {
            get
            {
                return _orderRepository ?? (_orderRepository = new GenericRepository<Model.Entities.Order>(_context));
            }
            set
            {
                _orderRepository = value;
            }
        }

        public GenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository ?? (_orderDetailRepository = new GenericRepository<OrderDetail>(_context));
            }
            set
            {
                _orderDetailRepository = value;
            }
        }

        public GenericRepository<Role> RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new GenericRepository<Role>(_context)); }
            set
            {
                _roleRepository = value;
            }
        }
        #endregion

        #region Property NorthWnd Repositories
        public NorthWindGenericRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ??
                       (_productRepository = new NorthWindGenericRepository<Product>(_nwndContext));
            }
            set
            {
                _productRepository = value;
            }
        }

        public NorthWindGenericRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ??
                       (_categoryRepository = new NorthWindGenericRepository<Category>(_nwndContext));
            }
            set
            {
                _categoryRepository = value;
            }
        }

        public NorthWindGenericRepository<Supplier> SupplierRepository
        {
            get
            {
                return _supplierRepository ??
                       (_supplierRepository = new NorthWindGenericRepository<Supplier>(_nwndContext));
            }
            set
            {
                _supplierRepository = value;
            }
        }

        public NorthWindGenericRepository<DAL.Order> NewOrderRepository
        {
            get
            {
                return _neworderRepository ??
                       (_neworderRepository = new NorthWindGenericRepository<DAL.Order>(_nwndContext));
            }
            set
            {
                _neworderRepository = value;
            }
        }

        public NorthWindGenericRepository<Shipper> ShipperRepository
        {
            get
            {
                return _shipperRepository ??
                       (_shipperRepository = new NorthWindGenericRepository<Shipper>(_nwndContext));
            }
            set
            {
                _shipperRepository = value;
            }
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
            _nwndContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();

                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
