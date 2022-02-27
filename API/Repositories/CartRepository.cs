using API.Data;
using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.IRepositories;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart> GetCart(string userId)
        {
            return await this.DbSet.FirstOrDefaultAsync(x=>x.UserId.Equals(userId));
        }
    }
}
