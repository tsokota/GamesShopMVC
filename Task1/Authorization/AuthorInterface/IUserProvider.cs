using Model.Entities;

namespace Yevhenii_KoliesnikTask1.Authorization.AuthorInterface
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
}
