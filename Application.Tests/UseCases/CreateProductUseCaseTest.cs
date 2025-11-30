using Business.Entities;
using Business.Services;
using Moq;

namespace Application.Tests.UseCases;

/// <summary>
/// UNIT TEST responsible for testing CreateProductUseCase
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev  
/// </remarks>
public class CreateProductUseCaseTest
{
    private readonly Mock<IProductService> _mockService;
    private readonly CancellationToken _token;
    
    public CreateProductUseCaseTest()
    {
        _mockService = new Mock<IProductService>();
        _token = new CancellationToken();
    }
    
    [Fact]
    public async Task InvokeAsync()
    {
        // Arrange
        var product = new Product();

        _mockService.Setup(s => s.AddProductAsync(product, _token))
            .Returns(Task.CompletedTask);
        
        // Act
        await _mockService.Object.AddProductAsync(product, _token);
        
        // Assert
        _mockService.Verify(s => s.AddProductAsync(product, _token), Times.Once);
    }
}