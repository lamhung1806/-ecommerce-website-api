using API.Service.Categories;
using API.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await categoryService.GetAll());
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchByName(string name)
        {
            return Ok(await categoryService.Search(name));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await categoryService.GetById(id);

            if(category != null)
                return Ok(category);

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Create(CategoryVm categoryVm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await categoryService.Create(categoryVm);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Update(CategoryVm categoryVm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await categoryService.Update(categoryVm);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isSuccess = await categoryService.Delete(id);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }
    }
}
