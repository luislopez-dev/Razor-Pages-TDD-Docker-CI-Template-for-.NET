using FluentValidation.Results;

namespace Application.Exceptions.Product.Exceptions;

/// <summary>
/// EXCEPTION FOR PRODUCT VALIDATION
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class ProductValidationException: ValidationException
{
    public ProductValidationException(List<ValidationFailure> failures) : base(failures)
    {
    }
}
