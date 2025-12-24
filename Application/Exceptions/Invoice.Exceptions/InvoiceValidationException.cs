using FluentValidation.Results;

namespace Application.Exceptions.Invoice.Exceptions;

/// <summary>
/// EXCEPTION FOR INVOICE VALIDATIONS
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class InvoiceValidationException: ValidationException
{
    public InvoiceValidationException(List<ValidationFailure> failures) : base(failures)
    {
    }
}