using Contracts;

namespace RepositoryUnitOfWork.Contract
{
    public interface IUnitOfWork
    {
        IPhotoRepository PhotoRepository{get;}
        IContactRepository ContactRepository { get;}
        IUserRepository UserRepository { get;}

        Task SaveAsync();
    }
}
