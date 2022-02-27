namespace API.Data.EntityBase.Entities
{
    public class CartDetails:EntityBase
    {
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
