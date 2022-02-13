using Ecommerce.REST.Models;
using System.Collections.Generic;

namespace Ecommerce.REST.Services.Contracts
{
    public interface IOrderService
    {
        void PlaceOrder(Order order);
        Order Get(long id);
        List<Order> GetAll();
    }
}
