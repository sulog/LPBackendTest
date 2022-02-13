using Ecommerce.REST.Models;
using Ecommerce.REST.Repositories.Contracts;
using Ecommerce.REST.Services.Contracts;
using System.Collections.Generic;

namespace Ecommerce.REST.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Get(long id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> Search(string searchText)
        {
            return _productRepository.Search(searchText);
        }
    }
}
