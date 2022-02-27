using API.Service.Orders;
using API.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await orderService.GetAll());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await orderService.GetById(id);
            if(order != null)
                return Ok(order);

            return BadRequest();
        }

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var order = await orderService.GetByUserId(userId);
            if (order != null)
                return Ok(order);

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateOrder createOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await orderService.Create(createOrder);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Update(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await orderService.Update(id);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Reject(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            bool isSuccess = await orderService.Reject(id);

            if (isSuccess)
                return Ok(isSuccess);

            return BadRequest();
        }
    }
}
