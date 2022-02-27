using API.Service.Carts;
using API.ViewModels.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var carts = await cartService.GetCart(userId);
            if(carts == null)
                return NotFound();

            return Ok(carts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCart createCart)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await cartService.Create(createCart);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CreateCart createCart)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await cartService.Update(createCart);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> Delete(string userId, int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await cartService.Delete(userId, productId);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }
    }
}
