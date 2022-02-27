using API.Data;
using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.IRepositories;

namespace API.Repositories
{
    public class CartDetailRepository : GenericRepository<CartDetails>, ICartDetailRepository
    {
        public CartDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void DeleteVer1(CartDetails cart)
        {
            this.DbSet.Remove(cart);
        }
    }
}
