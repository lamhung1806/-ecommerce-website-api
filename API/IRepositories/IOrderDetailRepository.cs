using API.Data.EntityBase.Entities;
using API.Infastructures;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.IRepositories
{
    public interface IOrderDetailRepository: IGenericRepository<OrderDetail>
    {
        Task<IList<OrderDetail>> GetAllInclude(Expression<Func<OrderDetail, bool>> condition);
    }
}
