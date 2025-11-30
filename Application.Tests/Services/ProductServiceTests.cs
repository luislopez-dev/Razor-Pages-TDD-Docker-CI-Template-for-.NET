using Application.Repositories;
using Application.Services;
using Business.Entities;
using Business.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Application.Tests.Services;

/// <summary>
/// UNIT TEST responsible for testing PRODUCT SERVICE
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev  
/// </remarks>
public class ProductServiceTests
{
    private readonly Mock<IValidator<Product>> _mockValidator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _mockValidator = new Mock<IValidator<Product>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockProductRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockUnitOfWork.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task AddProductAsync_ValidProduct_AddsProductToRepository()
    {
        // Arrange
        var product = new Product { Guid = new Guid(), Name = "Cereal Kellogs 200 mg", 
                                    Price = 30, Stock = 100, 
                                    Description = "Mutritivo Cereal Kellogs 200 mg"};

        var cancellationToken = CancellationToken.None;

        var validationResult = new ValidationResult();

        _mockValidator.Setup(v =>
                v.ValidateAsync(product, cancellationToken))
            .ReturnsAsync(validationResult);

        _mockUnitOfWork.Setup(ufw => ufw
                .ProductRepository.AddProductAsync(product, cancellationToken))
                .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(uow =>
            uow.CompleteAsync(cancellationToken))
            .ReturnsAsync(true);

        // Act
        await _productService.AddProductAsync(product, cancellationToken);

        // Assert
        _mockValidator.Verify(v => 
            v.ValidateAsync(product, cancellationToken), Times.Once);

        _mockUnitOfWork.Verify(ufw => 
            ufw.ProductRepository.AddProductAsync(product, cancellationToken), Times.Once);
        
        _mockUnitOfWork.Verify(uow => 
            uow.CompleteAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteProductByGuidAsync_ProductDeleted_DeletesProductFromRepository()
    {
        // Arrange
        var guid = new Guid();
        var cancellationToken = CancellationToken.None;

        _mockUnitOfWork.Setup(ufw =>
            ufw.ProductRepository
                .DeleteProductByGuidAsync(guid, cancellationToken))
            .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(ufw => 
            ufw.CompleteAsync(cancellationToken))
            .ReturnsAsync(true);

        // Act
        await _productService.DeleteProductByGuidAsync(guid, cancellationToken);

        // Assert
        _mockUnitOfWork.Verify(ufw => 
            ufw.ProductRepository
                .DeleteProductByGuidAsync(guid, cancellationToken), Times.Once);
        
        _mockUnitOfWork.Verify(ufw => 
            ufw.CompleteAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public void UpdateProduct_ProductUpdated_UpdatesProductFromRepository()
    {
        // Arrange
        var product = new Product { Guid = new Guid(), Name = "Cereal Kellogs 200 mg", 
            Price = 30, Stock = 100, 
            Description = "Mutritivo Cereal Kellogs 200 mg"};

        var cancellationToken = CancellationToken.None;

        var validationResult = new ValidationResult();

        _mockValidator.Setup(v =>
            v.ValidateAsync(product, cancellationToken))
            .ReturnsAsync(validationResult);

        _mockUnitOfWork.Setup(ufw =>
            ufw.ProductRepository
                .UpdateProduct(product, cancellationToken));

        _mockUnitOfWork.Setup(ufw =>
                ufw.CompleteAsync(cancellationToken))
            .ReturnsAsync(true);

        // Act
        _productService.UpdateProduct(product, cancellationToken);
        
        // Assert
        _mockValidator.Verify(v => 
            v.ValidateAsync(product, cancellationToken));
        
        _mockUnitOfWork.Verify(ufw => 
            ufw.ProductRepository
                .UpdateProduct(product, cancellationToken));
        
        _mockUnitOfWork.Verify(ufw => 
            ufw.CompleteAsync(cancellationToken));
    }

    [Fact]
    public async Task GetProductsPaginatedAsync_PaginatedProductsReturned_ReturnsPaginatedProductsFromRepository()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var products = new List<Product>
        {
            new Product(), new Product()
        };

        _mockUnitOfWork.Setup(ufw =>
            ufw.ProductRepository
                .GetProductsPaginatedAsync(cancellationToken))
            .ReturnsAsync(products);

        // Act
        await _productService
            .GetProductsPaginatedAsync(cancellationToken);

        // Assert
        _mockUnitOfWork.Setup(ufw =>
            ufw.ProductRepository
                .GetProductsPaginatedAsync(cancellationToken));
    }

    [Fact]
    public async Task GetProductByIdAsync_ProductReturned_ReturnsProductByIdFromRepository()
    {
        // Arrange
        int id = 1;
        var product = new Product();
        var cancellationToken = CancellationToken.None;

        _mockUnitOfWork.Setup(ufw =>
                ufw.ProductRepository
                    .GetProductByIdAsync(id, cancellationToken))
            .ReturnsAsync(product);

        // Act
        await _productService
            .GetProductByIdAsync(id, cancellationToken);

        // Assert
        _mockUnitOfWork.Verify(ufw => 
            ufw.ProductRepository.GetProductByIdAsync(id, cancellationToken));
    }

    [Fact]
    public async Task GetProductByGuidAsync_ProductReturned_ReturnsProductByGuidFromRepository()
    {
        // Arrange
        var guid = new Guid();
        var product = new Product();
        var cancellationToken = CancellationToken.None;

        _mockUnitOfWork.Setup(ufw => ufw
                .ProductRepository
                .GetProductByGuidAsync(guid, cancellationToken))
            .ReturnsAsync(product);

        // Act
        await _productService
            .GetProductByGuidAsync(guid, cancellationToken);

        // Assert
        _mockUnitOfWork.Verify(ufw => 
            ufw.ProductRepository
                .GetProductByGuidAsync(guid, cancellationToken));
    }

    [Fact]
    public async Task GetProductsByNamePaginatedAsync_PaginatedProductsReturned_ReturnsPaginatedProductsByNameFromRepository()
    {
        // Arrange
        var products = new List<Product> { new Product(), new Product() };
        var cancellationToken = CancellationToken.None;
        string name = "bebida";

        _mockUnitOfWork.Setup(ufw =>
                ufw.ProductRepository
                    .GetProductsByNamePaginated(name, cancellationToken))
            .ReturnsAsync(products);

        // Act
        await _productService
            .GetProductsByNamePaginatedAsync(name, cancellationToken);

        // Assert
        _mockUnitOfWork.Verify(ufw => 
            ufw.ProductRepository
                .GetProductsByNamePaginated(name, cancellationToken));
    }
}

/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open source Project
 */
