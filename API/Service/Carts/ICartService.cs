using API.ViewModels.Carts;
using API.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service.Carts
{
    public interface ICartService
    {
        Task<IList<ViewProduct>> GetCart(string userId);

        Task<bool> Create(CreateCart createCart);

        Task<bool> Update(CreateCart createCart);

        Task<bool> Delete(string userId, int productId);
    }
}