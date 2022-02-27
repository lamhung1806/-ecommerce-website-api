using API.Data;
using API.IRepositories;
using API.Repositories;
using System.Threading.Tasks;

namespace API.Infastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private ICategoryRepository categoryRepository;
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private ICartDetailRepository cartDetailRepository;
        private ICartRepository cartRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ApplicationDbContext ApplicationDbContext => this.context;

        public ICategoryRepository CategoryRepository => this.categoryRepository ??= new CategoryRepository(this.context);

        public IProductRepository ProductRepository => this.productRepository ??= new ProductRepository(this.context);

        public IOrderRepository OrderRepository => this.orderRepository ??= new OrderRepository(this.context);

        public IOrderDetailRepository OrderDetailRepository => this.orderDetailRepository ??= new OrderDetailRepository(this.context);

        public ICartDetailRepository CartDetailRepository => this.cartDetailRepository ??= new CartDetailRepository(this.context);

        public ICartRepository CartRepository => this.cartRepository ??= new CartRepository(this.context);

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}