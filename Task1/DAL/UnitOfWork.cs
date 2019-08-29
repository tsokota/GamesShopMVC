using Model;
using Model.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private GameStoreContext context = new GameStoreContext();
        private GenericRepository<Game> gameRepository;
        private GenericRepository<Genre> genreRepository;
        private GenericRepository<Comment> commentRepository;
        private GenericRepository<EntityView> viewRepository;

        public UnitOfWork()
        { }

     

        public GenericRepository<Game> GameRepository
        {
            get
            {

                if (this.gameRepository == null)
                {
                    this.gameRepository = new GenericRepository<Game>(context);
                }
                return gameRepository;
            }
            set
            {
                gameRepository = value;
            }
        }

        public GenericRepository<Genre> GenreRepository
        {
            get
            {

                if (this.genreRepository == null)
                {
                    this.genreRepository = new GenericRepository<Genre>(context);
                }
                return genreRepository;
            }
        }

        public GenericRepository<Comment> CommentRepository
        {
            get
            {

                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<Comment>(context);
                }
                return commentRepository;
            }
            set
            {
                commentRepository = value;
            }

        }

        public GenericRepository<EntityView> ViewRepository
        {
            get
            {

                if (this.viewRepository == null)
                {
                    this.viewRepository = new GenericRepository<EntityView>(context);
                }
                return viewRepository;
            }
            set
            {
                viewRepository = value;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
