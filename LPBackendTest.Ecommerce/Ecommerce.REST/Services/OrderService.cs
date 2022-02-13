using Ecommerce.REST.Models;
using Ecommerce.REST.Repositories.Contracts;
using Ecommerce.REST.Services.Contracts;
using System;
using System.Collections.Generic;

namespace Ecommerce.REST.Services
{
    public class OrderService : IOrderService
    {
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public void PlaceOrder(Order order)
        {
            foreach(var p in order.Products)
            {
                _productRepository.ReduceProductQuantity(p.Id);
            }
            Random rnd = new Random();
            order.Id = rnd.Next(1000000, 9999999);
            _orderRepository.CreateOrder(order);
        }

        public Order Get(long id)
        {
            return _orderRepository.Get(id);
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }
    }
}
