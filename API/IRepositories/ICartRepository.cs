using API.Data.EntityBase.Entities;
using API.Infastructures;
using System.Threading.Tasks;

namespace API.IRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<Cart> GetCart(string userId);
    }
}
