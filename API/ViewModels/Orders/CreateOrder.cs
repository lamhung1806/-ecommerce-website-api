using API.ViewModels.EntityBaseVM;
using API.ViewModels.Products;
using System.Collections.Generic;

namespace API.ViewModels.Orders
{
    public class CreateOrder: EntityBaseVm
    {
        public CreateOrder()
        {
            Products = new List<CreateOrderProduct>();
        }

        public string NameUser { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool? Status { get; set; }

        public string UserId { get; set; }

        public IList<CreateOrderProduct> Products { get; set; }
    }
}
