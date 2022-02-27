using API.Data;
using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<OrderDetail>> GetAllInclude(Expression<Func<OrderDetail, bool>> condition)
        {
            return await this.DbSet.Where(condition).Include(x=>x.Product).ToListAsync();
        }
    }
}
