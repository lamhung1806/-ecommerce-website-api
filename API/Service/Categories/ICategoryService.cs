using API.Data.EntityBase.Entities;
using API.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Service.Categories
{
    public interface ICategoryService
    {
        Task<IList<CategoryVm>> GetAll();

        Task<bool> Create(CategoryVm categoryVm);

        Task<CategoryVm> GetById(int id);

        Task<bool> Update(CategoryVm categoryVm);

        Task<bool> Delete(int id);

        Task<IList<CategoryVm>> Search(string categoryName);
    }
}