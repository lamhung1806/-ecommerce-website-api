using System.Collections.Generic;

namespace API.Data.EntityBase.Entities
{
    public class Order : EntityBase
    {
        public int Id { get; set; }

        public string NameUser { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool? Status { get; set; }

        public string? UserId { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}