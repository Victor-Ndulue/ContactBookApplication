using Contracts;
using Contracts.IEntityQueryRepository;
using Repositories.Common;
using Repositories.EntityQueryRepositories;
using Repositories.EntityRepositories;
using RepositoryUnitOfWork.Contract;

namespace RepositoryUnitOfWork.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IPhotoRepository _photoRepository;
        private IContactRepository _contactRepository;
        private IContactQueryRepository _contactQueryRepository;
        private IUserRepository _userRepository;
        private IUserQueryRepository _userQueryRepository;
        public UnitOfWork(DataContext dataContext )
        { 
            _dataContext = dataContext;
        }
        public IPhotoRepository PhotoRepository => _photoRepository ??= new PhotoRepository(_dataContext);

        public IContactRepository ContactRepository => _contactRepository??= new ContactRepository(_dataContext);
        public IContactQueryRepository  ContactQueryRepository=> _contactQueryRepository ??= new ContactQueryRepository(_dataContext);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dataContext);
        public IUserQueryRepository UserQueryRepository => _userQueryRepository ??= new UserQueryRepository(_dataContext);

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
