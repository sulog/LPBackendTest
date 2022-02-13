namespace Ecommerce.REST.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
}
