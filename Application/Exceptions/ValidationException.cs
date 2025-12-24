using FluentValidation.Results;

namespace Application.Exceptions;

/// <summary>
/// BASE EXCEPTION FOR VALIDATIONS
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class ValidationException: Exception
{
    public List<ValidationFailure> ValidationFailures { get; }

    public ValidationException(List<ValidationFailure> validationFailures)
    {
        ValidationFailures = validationFailures;
    }
}
