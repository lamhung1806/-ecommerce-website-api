using API.ViewModels.EntityBaseVM;

namespace API.ViewModels.Orders
{
    public class ViewOrderBasic: EntityBaseVm
    {
        public int Id { get; set; }

        public string NameUser { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool? Status { get; set; }

        public string UserId { get; set; }
    }
}
