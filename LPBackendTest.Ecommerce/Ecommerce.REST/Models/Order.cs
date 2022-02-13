using Ecommerce.REST.Enums;
using System.Collections.Generic;

namespace Ecommerce.REST.Models
{
    public class Order
    {
        public long Id { get; set; }
        public List<Product> Products { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
