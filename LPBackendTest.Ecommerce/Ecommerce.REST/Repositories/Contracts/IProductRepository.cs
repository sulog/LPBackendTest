using Ecommerce.REST.Models;
using System.Collections.Generic;

namespace Ecommerce.REST.Repositories.Contracts
{
    public interface IProductRepository
    {
        Product Get(long id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> Search(string searchTerm);
        bool IsProductExist(long id);
        bool IsProductAvailable(long id);
        void ReduceProductQuantity(long id);
    }
}
