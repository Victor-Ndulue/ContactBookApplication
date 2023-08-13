using Contracts;
using Contracts.Common;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityRepositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
