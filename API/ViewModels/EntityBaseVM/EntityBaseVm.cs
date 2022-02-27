using System;

namespace API.ViewModels.EntityBaseVM
{
    public class EntityBaseVm : IEntityBaseVm
    {
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}