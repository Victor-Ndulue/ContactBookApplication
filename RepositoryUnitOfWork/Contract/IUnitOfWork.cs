using Contracts;
using Contracts.IEntityQueryRepository;

namespace RepositoryUnitOfWork.Contract
{
    public interface IUnitOfWork
    {
        IPhotoRepository PhotoRepository{get;}
        IContactRepository ContactRepository { get;}
        IContactQueryRepository ContactQueryRepository { get;}
        IUserRepository UserRepository { get;}
        IUserQueryRepository UserQueryRepository { get;}

        Task SaveAsync();
    }
}
