using Ecommerce.REST.Models;
using System.Collections.Generic;

namespace Ecommerce.REST.Services.Contracts
{
    public interface IProductService
    {
        Product Get(long id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> Search(string searchTerm);
    }
}
