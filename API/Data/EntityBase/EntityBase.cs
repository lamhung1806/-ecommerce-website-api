using System;

namespace API.Data.EntityBase
{
    public class EntityBase : IEntityBase
    {
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}