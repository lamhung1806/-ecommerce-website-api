using System.Collections.Generic;

namespace API.Data.EntityBase.Entities
{
    public class Cart:EntityBase
    {
        public int Id { get; set; }

        public IEnumerable<CartDetails> CartDetails { get; set; }

        public string UserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
