using API.ViewModels.EntityBaseVM;
using API.ViewModels.Products;
using System.Collections.Generic;

namespace API.ViewModels.Orders
{
    public class ViewOrder: EntityBaseVm
    {
        public ViewOrder()
        {
            Products = new List<ViewProduct>();
        }

        public int Id { get; set; }

        public string NameUser { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool? Status { get; set; }

        public string UserId { get; set; }

        public IList<ViewProduct> Products { get; set; }
    }
}
