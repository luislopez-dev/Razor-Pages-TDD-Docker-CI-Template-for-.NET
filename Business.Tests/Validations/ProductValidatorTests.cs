using Business.Entities;
using Business.Validations;

namespace Business.Tests.Validations;

/// <summary>
/// UNIT TEST responsible for testing PRODUCT
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev  
/// </remarks>
public class ProductValidatorTests
{
    [Fact]
    public void ProductValidator_ReturnsError_WhenNameIsNull()
    {
        // Arrange
        var product = new Product { Name = null, Price = 10, Description = "some description", Stock = 100 };
        var validator = new ProductValidator();

        // Act
        var result = validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        
        Assert.Contains(result.Errors, e => 
            e.PropertyName == "Name" && 
            e.ErrorMessage == "¡El nombre del producto no debe estar vacio!");
    }
}
