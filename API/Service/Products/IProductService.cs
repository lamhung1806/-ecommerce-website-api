using API.Data.EntityBase.Entities;
using API.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Service.Products
{
    public interface IProductService
    {
        Task<IList<ViewProduct>> GetAll();

        Task<IList<ViewProduct>> GetQuantityWarning();

        Task<IList<ViewProduct>> GetBestSold();

        Task<IList<ViewProduct>> GetLast();

        Task<IList<Statistical>> Statistical();

        Task<IList<ViewProduct>> GetPromotioPriceBest();

        Task<IList<ViewProduct>> GetAllDistinct();

        Task<bool> Create(CreateProduct createProduct);

        Task<ViewProduct> GetById(int id);

        Task<ViewProduct> GetBySize(string size, string productName, string color);

        Task<ViewProduct> GetByColor(string color, string productName, string size);

        Task<bool> Update(ViewProduct createProduct);

        Task<bool> Delete(int id);

        Task<IList<ViewProduct>> Search(string name);

        Task<IList<ViewProduct>> GetByCategory(int categoryId);
    }
}