using System;

namespace API.ViewModels.EntityBaseVM
{
    public interface IEntityBaseVm
    {
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}