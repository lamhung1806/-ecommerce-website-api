using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.ViewModels.Categories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Create(CategoryVm categoryVm)
        {
            try
            {
                var category = mapper.Map<Category>(categoryVm);

                await this.unitOfWork.CategoryRepository.Add(category);
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
                unitOfWork.CategoryRepository.Delete(id);
                await unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<CategoryVm>> Search(string categoryName)
        {
            var categories = await this.unitOfWork.CategoryRepository.Find(x => x.Name.Contains(categoryName));
            var categoryVms = new List<CategoryVm>();

            foreach (var category in categories)
            {
                categoryVms.Add(mapper.Map<CategoryVm>(category));
            }
            return categoryVms;
        }

        public async Task<IList<CategoryVm>> GetAll()
        {
            var categories = await this.unitOfWork.CategoryRepository.GetAll();
            var cagoryVms = new List<CategoryVm>();

            foreach (var category in categories)
            {
                cagoryVms.Add(mapper.Map<CategoryVm>(category));
            }
            return cagoryVms;
        }

        public async Task<CategoryVm> GetById(int id)
        {
            var category = await unitOfWork.CategoryRepository.GetById(id);
            var categoryVm = mapper.Map<CategoryVm>(category);

            return categoryVm;
        }

        public async Task<bool> Update(CategoryVm categoryVm)
        {
            try
            {
                var category = mapper.Map<Category>(categoryVm);

                this.unitOfWork.CategoryRepository.Update(category);
                await this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
