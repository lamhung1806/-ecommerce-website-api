using API.Data;
using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.IRepositories;

namespace API.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}