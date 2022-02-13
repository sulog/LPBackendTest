using Ecommerce.REST.Models;
using System.Collections.Generic;

namespace Ecommerce.REST.Services.Contracts
{
    public interface IValidationService
    {
        List<ValidationResult> OrderValidation(Order order);
    }
}
