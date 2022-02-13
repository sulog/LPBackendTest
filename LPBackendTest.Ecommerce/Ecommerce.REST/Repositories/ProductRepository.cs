using Ecommerce.REST.Models;
using Ecommerce.REST.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.REST.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly Dictionary<long, long> _productQuantity = new Dictionary<long, long>();
        
        public ProductRepository()
        {
            _products.Add(new Product() { Id = 1001, Category = "Book", Description = "About Will Smith's life", Name = "Will Hardcover", Price = 35.47, Rating = 4.7 });
            _products.Add(new Product() { Id = 1002, Category = "Book", Description = "The Drivers and Derailers of Leadership", Name = "Personality at Work", Price = 36.95, Rating = 4.5 });
            _products.Add(new Product() { Id = 1003, Category = "Book", Description = "A Journey Into the Mysterious System That Keeps You Alive", Name = "Immune", Price = 37.99, Rating = 5 });
            _products.Add(new Product() { Id = 1004, Category = "Book", Description = "The Final Edition Paperback", Name = "Nudge", Price = 36.95, Rating = 4.5 });
            _products.Add(new Product() { Id = 1005, Category = "Pen", Description = "Fountain Pen", Name = "Crown", Price = 5.0, Rating = 3 });
            _products.Add(new Product() { Id = 1006, Category = "Pen", Description = "Ballpoint Pen", Name = "Muji Knock", Price = 3.55, Rating = 4.5 });
            _products.Add(new Product() { Id = 1007, Category = "Pen", Description = "Gel Pen", Name = "Pilot", Price = 4.2, Rating = 5 });

            _productQuantity.Add(1001, 10);
            _productQuantity.Add(1002, 10);
            _productQuantity.Add(1003, 10);
            _productQuantity.Add(1004, 0);
            _productQuantity.Add(1005, 10);
            _productQuantity.Add(1006, 10);
            _productQuantity.Add(1007, 0);
        }

        public Product Get(long id)
        {
            return _products.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public IEnumerable<Product> Search(string searchTerm)
        {
            return _products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Category.ToLower().Contains(searchTerm));
        }

        /// <summary>
        /// To check whether product exist
        /// true - valid product
        /// false - invalid product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsProductExist(long id)
        {
            return _products.Where(x => x.Id == id).FirstOrDefault() != null;
        }

        /// <summary>
        /// To check whether product available
        /// true - available to order
        /// false - unavailable or product does not exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsProductAvailable(long id)
        {
            var productExist = IsProductExist(id);

            if (!productExist)
                return false;

            if (!_productQuantity.ContainsKey(id))
                return false;

            return _productQuantity[id] > 0;
        }

        public void ReduceProductQuantity(long id)
        {
            _productQuantity[id]--;
        }
    }
}
