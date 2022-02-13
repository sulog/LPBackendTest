using Ecommerce.REST.Models;
using System.Collections.Generic;

namespace Ecommerce.REST.Repositories.Contracts
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Order Get(long id);
        List<Order> GetAll();
    }
}
