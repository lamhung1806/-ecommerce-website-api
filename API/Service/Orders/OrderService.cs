using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.ViewModels.Orders;
using API.ViewModels.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Service.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Create(CreateOrder createOrder)
        {
            try
            {
                var order = mapper.Map<Order>(createOrder);

                await this.unitOfWork.OrderRepository.Add(order);
                await this.unitOfWork.SaveChanges();

                foreach (var item in createOrder.Products)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.ProductId = item.Id;
                    orderDetail.Quantity = item.Quantity;

                    var productExisting = await this.unitOfWork.ProductRepository.GetById(item.Id);

                    if (productExisting.Quantity >= item.Quantity) {
                        productExisting.Quantity -= item.Quantity;
                        productExisting.Sold += item.Quantity;
                    }
                        
                    else
                    {
                        this.unitOfWork.OrderRepository.Delete(order.Id);
                        await this.unitOfWork.SaveChanges();
                        return false;
                    }
                       
                    this.unitOfWork.ProductRepository.Update(productExisting);

                    await this.unitOfWork.OrderDetailRepository.Add(orderDetail);

                    await this.unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ViewOrder> GetById(int id)
        {
            var order = await this.unitOfWork.OrderRepository.GetById(id);
            var orderDetails = await this.unitOfWork.OrderDetailRepository.Find(x=>x.OrderId == order.Id);

            var viewOrder = mapper.Map<ViewOrder>(order);

            foreach (var orderDetail in orderDetails)
            {
                var product = mapper.Map<ViewProduct>(await this.unitOfWork.ProductRepository.GetById(orderDetail.ProductId));
                product.Quantity = orderDetail.Quantity;
                viewOrder.Products.Add(product);
            }

            return viewOrder;
        }

        public async Task<IList<ViewOrderBasic>> GetAll()
        {
            var orders = await this.unitOfWork.OrderRepository.GetAll();

            IList<ViewOrderBasic> viewOrders = new List<ViewOrderBasic>();

            foreach (var item in orders)
            {
                var viewOrder = mapper.Map<ViewOrderBasic>(item);

                viewOrders.Add(viewOrder);
            }

            return viewOrders;
        }

        public async Task<bool> Update(int id)
        {
            try
            {
                var order = await this.unitOfWork.OrderRepository.GetById(id);
                order.Status = true;
                this.unitOfWork.OrderRepository.Update(order);
                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Reject(int id)
        {
            try
            {
                var order = await this.unitOfWork.OrderRepository.GetById(id);
                order.Status = null;
                this.unitOfWork.OrderRepository.Update(order);
                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<ViewOrderBasic>> GetByUserId(string userId)
        {
            var orders = await this.unitOfWork.OrderRepository.Find(x=>x.UserId.Equals(userId));

            IList<ViewOrderBasic> viewOrders = new List<ViewOrderBasic>();

            foreach (var item in orders)
            {
                var viewOrder = mapper.Map<ViewOrderBasic>(item);

                viewOrders.Add(viewOrder);
            }

            return viewOrders;
        }
    }
}
