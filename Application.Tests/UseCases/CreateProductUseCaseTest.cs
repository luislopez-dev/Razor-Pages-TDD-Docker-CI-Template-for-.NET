/*
 * Author: Luis René López
 * Website: https://github.com/luislopez-dev
 * Description: Open Source Project
 */

using Application.Services;
using Business.Interfaces;
using Business.Models;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Application.Tests.UseCases;


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

/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open Source Project
 */