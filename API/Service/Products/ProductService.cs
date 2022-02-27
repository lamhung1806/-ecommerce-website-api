using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.ViewModels.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Create(CreateProduct createProduct)
        {
            try
            {
                var product = mapper.Map<Product>(createProduct);

                await this.unitOfWork.ProductRepository.Add(product);
                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                unitOfWork.ProductRepository.Delete(id);
                await unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<ViewProduct>> Search(string name)
        {
            var products = await this.unitOfWork.ProductRepository.Find(x=>x.Name.Contains(name));
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms.GroupBy(x=>x.Name).Select(y=>y.First()).ToList();
        }

        public async Task<IList<ViewProduct>> GetByCategory(int categoryId)
        {
            var products = await this.unitOfWork.ProductRepository.Find(x => x.CategoryId == categoryId);
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms.GroupBy(x=>x.Name).Select(y=>y.First()).ToList();
        }

        public async Task<IList<ViewProduct>> GetAllDistinct()
        {
            var products = await this.unitOfWork.ProductRepository.GetAll();
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms.GroupBy(x=>x.Name).Select(y=>y.First()).ToList();
        }

        public async Task<ViewProduct> GetById(int id)
        {
            var product = await unitOfWork.ProductRepository.GetById(id);
            var productVm = mapper.Map<ViewProduct>(product);

            return productVm;
        }

        public async Task<bool> Update(ViewProduct createProduct)
        {
            try
            {
                var product = mapper.Map<Product>(createProduct);

                this.unitOfWork.ProductRepository.Update(product);
                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ViewProduct> GetBySize(string size, string productName, string color)
        {
            var products = await unitOfWork.ProductRepository.Find(x=>x.Size.ToLower().Equals(size.ToLower()) && x.Name.ToLower().Equals(productName.ToLower()));
            var productVm= new ViewProduct();
            foreach (var item in products)
            {
                if (item.Color.ToLower().Equals(color.ToLower()))
                {
                    productVm = mapper.Map<ViewProduct>(item);
                    break;
                }

                productVm = mapper.Map<ViewProduct>(item);
            }

            return productVm;
        }

        public async Task<ViewProduct> GetByColor(string color, string productName, string size)
        {
            var products = await unitOfWork.ProductRepository.Find(x => x.Color.ToLower().Equals(color.ToLower()) && x.Name.ToLower().Equals(productName.ToLower()));
            var productVm = new ViewProduct();

            foreach (var item in products)
            {
                if (item.Size.ToLower().Equals(size.ToLower()))
                {
                    productVm = mapper.Map<ViewProduct>(item);
                    break;
                }

                productVm = mapper.Map<ViewProduct>(item);
            }

            return productVm;
        }

        public async Task<IList<ViewProduct>> GetAll()
        {
            var products = await this.unitOfWork.ProductRepository.GetAll();
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms;
        }

        public async Task<IList<ViewProduct>> GetBestSold()
        {
            var products = await this.unitOfWork.ProductRepository.GetAll();
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms.GroupBy(x => x.Name).Select(x => x.First()).OrderByDescending(x => x.Sold).Take(8).ToList();
        }

        public async Task<IList<ViewProduct>> GetLast()
        {
            var products = await this.unitOfWork.ProductRepository.GetAll();
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms.GroupBy(x=>x.Name).Select(x=>x.First()).OrderByDescending(x=>x.CreatedOn).Take(8).ToList();
        }

        public async Task<IList<ViewProduct>> GetPromotioPriceBest()
        {
            var products = await this.unitOfWork.ProductRepository.GetAll();
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms.GroupBy(x => x.Name).Select(x => x.First()).OrderByDescending(x => x.PromotionPrice).Take(8).ToList();
        }

        public async Task<IList<ViewProduct>> GetQuantityWarning()
        {
            var products = await this.unitOfWork.ProductRepository.Find(x=>x.Quantity < 20);
            var productVms = new List<ViewProduct>();

            foreach (var product in products)
            {
                productVms.Add(mapper.Map<ViewProduct>(product));
            }
            return productVms;
        }

        public async Task<IList<Statistical>> Statistical()
        {
            var orderDtails = await this.unitOfWork.OrderDetailRepository.GetAllInclude(x=>x.CreatedOn.Year == DateTime.Now.Year);

            return orderDtails.GroupBy(x => x.CreatedOn.Month)
                                .Select(s => new Statistical
                                {
                                    Month = s.First().CreatedOn.Month,
                                    Sales = s.Sum(x => x.Quantity * ((x.Product.Price * (100 - x.Product.PromotionPrice))/100))
                                }).OrderBy(x => x.Month).ToList();
        }
    }
}
