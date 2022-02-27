using System.Collections.Generic;

namespace API.Data.EntityBase.Entities
{
    public class Category : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}