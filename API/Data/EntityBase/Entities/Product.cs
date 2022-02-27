using System.Collections.Generic;

namespace API.Data.EntityBase.Entities
{
    public class Product : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public int Sold { get; set; }

        public decimal Price { get; set; }

        public int PromotionPrice { get; set; }

        public bool Status { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}