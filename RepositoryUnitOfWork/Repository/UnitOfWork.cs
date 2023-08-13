using Contracts;
using Contracts.Common;
using Models.Entities;
using Repositories.Common;
using Repositories.EntityRepositories;
using RepositoryUnitOfWork.Contract;

namespace RepositoryUnitOfWork.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IPhotoRepository _photoRepository;
        private IContactRepository _contactRepository;
        private IUserRepository _userRepository;
        public UnitOfWork(DataContext dataContext )
        { 
            _dataContext = dataContext;
        }
        public IPhotoRepository PhotoRepository => _photoRepository ??= new PhotoRepository(_dataContext);

        public IContactRepository ContactRepository => _contactRepository??= new ContactRepository(_dataContext);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dataContext);

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
