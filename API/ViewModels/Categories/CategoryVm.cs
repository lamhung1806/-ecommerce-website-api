using API.ViewModels.EntityBaseVM;

namespace API.ViewModels.Categories
{
    public class CategoryVm : EntityBaseVm
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}