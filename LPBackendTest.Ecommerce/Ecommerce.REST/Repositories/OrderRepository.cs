using Ecommerce.REST.Models;
using Ecommerce.REST.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.REST.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> _orders = new List<Order>();

        public void CreateOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order Get(long id)
        {
            return _orders.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Order> GetAll()
        {
            return _orders;
        }
    }
}
