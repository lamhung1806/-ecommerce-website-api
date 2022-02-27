using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.ViewModels.Carts;
using API.ViewModels.Products;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service.Carts
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Create(CreateCart createCart)
        {
            try
            {
                var cart = await this.unitOfWork.CartRepository.GetCart(createCart.UserId);
                
                var cartDetail = new CartDetails()
                {
                    CartId = cart.Id,
                    ProductId = createCart.ProductId,
                    Quantity = createCart.Quantity,
                };

                await this.unitOfWork.CartDetailRepository.Add(cartDetail);
                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(string userId, int productId)
        {
            try
            {
                var cart = await this.unitOfWork.CartRepository.GetCart(userId);

                var cartDetails = await this.unitOfWork.CartDetailRepository.Find(x=>x.CartId == cart.Id);

                foreach(var item in cartDetails)
                {
                    if (item.ProductId == productId)
                        this.unitOfWork.CartDetailRepository.DeleteVer1(item);
                }

                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch
            {

                return false;
            }
            
        }

        public async Task<IList<ViewProduct>> GetCart(string userId)
        {
            var cart = await this.unitOfWork.CartRepository.GetCart(userId);

            if (cart == null)
            {
                return null;
            }

            var cartDetails = await this.unitOfWork.CartDetailRepository.Find(x => x.CartId == cart.Id);
            var products = new List<ViewProduct>();

            foreach (var cartDetail in cartDetails)
            {
                var product = mapper.Map<ViewProduct>(await this.unitOfWork.ProductRepository.GetById(cartDetail.ProductId));
                product.Quantity = cartDetail.Quantity;
                products.Add(product);
            }

            return products;
        }

        public async Task<bool> Update(CreateCart createCart)
        {
            try
            {
                var cart = await this.unitOfWork.CartRepository.GetCart(createCart.UserId);

                var cartDetails = await this.unitOfWork.CartDetailRepository.Find(x=>x.CartId == cart.Id && x.ProductId == createCart.ProductId);
                if (cartDetails == null)
                    return false;
               
                foreach(var item in cartDetails)
                {
                    item.Quantity = createCart.Quantity;
                    this.unitOfWork.CartDetailRepository.Update(item);
                    await this.unitOfWork.SaveChanges();
                    break;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
