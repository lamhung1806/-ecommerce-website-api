using API.Service.Products;
using API.ViewModels.Products;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productService.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Statistical()
        {
            return Ok(await productService.Statistical());
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetQuantityWarning()
        {
            return Ok(await productService.GetQuantityWarning());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBestSold()
        {
            return Ok(await productService.GetBestSold());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLast()
        {
            return Ok(await productService.GetLast());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBestPromotion()
        {
            return Ok(await productService.GetPromotioPriceBest());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDistinct()
        {
            return Ok(await productService.GetAllDistinct());
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchByName(string name)
        {
            return Ok(await productService.Search(name));
        }

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchByCategory(int categoryId)
        {
            return Ok(await productService.GetByCategory(categoryId));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetById(id);

            if (product != null)
                return Ok(product);

            return BadRequest();
        }

        [HttpGet("{size}/{productName}/{color}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySize(string size, string productName, string color)
        {
            var product = await productService.GetBySize(size, productName, color);

            if (product != null)
                return Ok(product);

            return BadRequest();
        }


        [HttpGet("{color}/{productName}/{size}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByColor(string color, string productName, string size)
        {
            var product = await productService.GetByColor(color, productName, size);

            if (product != null)
                return Ok(product);

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Create(CreateProduct createProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await productService.Create(createProduct);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Update(ViewProduct createProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await productService.Update(createProduct);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isSuccess = await productService.Delete(id);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }
    }
}
