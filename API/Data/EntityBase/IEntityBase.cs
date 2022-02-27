using System;

namespace API.Data.EntityBase
{
    public interface IEntityBase
    {
        DateTime CreatedOn { get; set; }

        DateTime UpdatedOn { get; set; }
    }
}