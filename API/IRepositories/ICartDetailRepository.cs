using API.Data.EntityBase.Entities;
using API.Infastructures;

namespace API.IRepositories
{
    public interface ICartDetailRepository : IGenericRepository<CartDetails>
    {
        void DeleteVer1(CartDetails cart);
    }
}
