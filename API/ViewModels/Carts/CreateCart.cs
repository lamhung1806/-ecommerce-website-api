using API.ViewModels.Orders;
using System.Collections.Generic;

namespace API.ViewModels.Carts
{
    public class CreateCart
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
