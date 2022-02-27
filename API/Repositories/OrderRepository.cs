using API.Data;
using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.IRepositories;

namespace API.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
