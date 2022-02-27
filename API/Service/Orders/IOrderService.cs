using API.Data.EntityBase.Entities;
using API.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Service.Orders
{
    public interface IOrderService
    {
        Task<IList<ViewOrderBasic>> GetAll();

        Task<IList<ViewOrderBasic>> GetByUserId(string userId);

        Task<bool> Create(CreateOrder createOrder);

        Task<bool> Update(int id);

        Task<bool> Reject(int id);

        Task<ViewOrder> GetById(int id);
    }
}