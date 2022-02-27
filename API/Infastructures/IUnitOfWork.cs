using API.Data;
using API.IRepositories;
using System;
using System.Threading.Tasks;

namespace API.Infastructures
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        IOrderRepository OrderRepository { get; }

        IOrderDetailRepository OrderDetailRepository { get; }

        ICartRepository CartRepository { get; }

        ICartDetailRepository CartDetailRepository { get; }

        ApplicationDbContext ApplicationDbContext { get; }

        Task<int> SaveChanges();
    }
}