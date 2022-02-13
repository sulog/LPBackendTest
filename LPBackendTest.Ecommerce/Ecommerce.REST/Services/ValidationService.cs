using Ecommerce.REST.Constants;
using Ecommerce.REST.Models;
using Ecommerce.REST.Repositories.Contracts;
using Ecommerce.REST.Services.Contracts;
using System.Collections.Generic;

namespace Ecommerce.REST.Services
{
    public class ValidationService : IValidationService
    {
        private IProductRepository _productRepository;
        public ValidationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ValidationResult> OrderValidation(Order order)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            foreach (var product in order.Products)
            {
                if (!_productRepository.IsProductExist(product.Id))
                {
                    result.Add(new ValidationResult() { ErrorCode = APIConstants.InvalidProductErrorCode, ErrorMessage = string.Format(APIConstants.InvalidProductErrorMessage, product.Id) });
                }
                else
                {
                    if (!_productRepository.IsProductAvailable(product.Id))
                    {
                        result.Add(new ValidationResult() { ErrorCode = APIConstants.ProductNotAvailableErrorCode, ErrorMessage = string.Format(APIConstants.ProductNotAvailableErrorMessage, product.Id) });
                    }
                }
            }
            return result;
        }
    }
}
