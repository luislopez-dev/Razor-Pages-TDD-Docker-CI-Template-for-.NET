using Application.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

/// <summary>
/// INTEGRATION TEST responsible for testing PRODUCT REPOSITORY
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev  
/// </remarks>
public class ProductRepositoryIntegrationTests: IDisposable
{
    private readonly CancellationToken _cancellationToken;
    private readonly DataContext _dataContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _repository;

    private DbContextOptions<DataContext> CreateInMemoryDatabaseOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "Test Database")
            .Options;
    }
    
    public ProductRepositoryIntegrationTests()
    {
        _cancellationToken = CancellationToken.None;
        var options = CreateInMemoryDatabaseOptions();
        _dataContext = new DataContext(options);
        _repository = new ProductRepository(_dataContext);
        _unitOfWork = new UnitOfWork(_dataContext);
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);

        // Arrange
        var product = new Product
        {
            Id = 1, Guid = Guid.NewGuid(),
            Name = "Cereal Kellogs 600gm",
            Price = 40, Stock = 100,
            Description = "Some description"
        };

        // Act
        await _repository.AddProductAsync(product, _cancellationToken);
        await _unitOfWork.CompleteAsync(_cancellationToken);

        // Assert
        var savedProduct = await _dataContext
            .Products
            .FirstOrDefaultAsync(p => p.Name == "Cereal Kellogs 600gm");

        Assert.NotNull(savedProduct);
        Assert.Equal(40, savedProduct.Price);
        Assert.Equal(100, savedProduct.Stock);
        Assert.Equal("Some description", savedProduct.Description);
    }

    [Fact]
    public async Task DeleteProductByGuidAsync_ShouldDeleteProduct()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);

        // Arrange
        var guid = Guid.NewGuid();
        var product = new Product
        {
            Id = 1, 
            Guid = guid,
            Name = "Cereal Kellogs 600gm",
            Price = 40, Stock = 100,
            Description = "Some description"
        };
        
        await _repository
            .AddProductAsync(product, _cancellationToken);
        
        await _unitOfWork
            .CompleteAsync(_cancellationToken);

        var productCreated = await _repository
            .GetProductByGuidAsync(guid, _cancellationToken);

        Assert.NotNull(productCreated);

        // Act
        await _repository
            .DeleteProductByGuidAsync(guid, _cancellationToken);
        
        await _unitOfWork.CompleteAsync(_cancellationToken);
        
        // Assert
        var deletedProduct = await _repository.GetProductByGuidAsync(guid, _cancellationToken);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task UpdateProduct_ShouldUpdateProduct()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);

        // Arrange
        var guid = Guid.NewGuid();
        var product = new Product
        {
            Id = 1, 
            Guid = guid,
            Name = "Cereal Kellogs 600gm",
            Price = 40, 
            Stock = 100,
            Description = "Some description"
        };
        
        await _repository
            .AddProductAsync(product, _cancellationToken);
        
        await _unitOfWork
            .CompleteAsync(_cancellationToken);

        var productCreated = await _repository
            .GetProductByGuidAsync(guid, _cancellationToken);

        Assert.NotNull(productCreated);
        
        // Act
        var newProduct = new Product()
        {
            Id = 1,
            Guid = guid,
            Name = "Cereal Kellogs 600gm",
            Price = 30, 
            Stock = 50,
            Description = "Description Updated"
        };
        
        _dataContext.Entry(productCreated).State = EntityState.Detached;
        
        _repository.UpdateProduct(newProduct, _cancellationToken);
        await _dataContext.SaveChangesAsync(_cancellationToken);

        // Assert
        var productUpdated = await _repository
            .GetProductByGuidAsync(guid, _cancellationToken);
        
        Assert.NotNull(productUpdated);
        Assert.Equal(30, productUpdated.Price);
        Assert.Equal(50, productUpdated.Stock);
        Assert.Equal("Description Updated", productUpdated.Description);
    }

    [Fact]
    public async Task GetProductsPaginatedAsync_ShouldReturnPaginatedProducts()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);
        
        // Arrange
        var product1 = new Product();
        var product2 = new Product();

        await _repository
            .AddProductAsync(product1, _cancellationToken);

        await _repository
            .AddProductAsync(product2, _cancellationToken);

        await _dataContext.SaveChangesAsync(_cancellationToken);

        // Act
        var createdProducts = await _repository
            .GetProductsPaginatedAsync(_cancellationToken);

        // Assert
        Assert.Equal(2, createdProducts.Count);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProductById()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);
        
        // Arrange
        var product = new Product { Id = 100 };
        await _repository.AddProductAsync(product, _cancellationToken);
        await _dataContext.SaveChangesAsync(_cancellationToken);

        // Act
        var productFound = await _repository
            .GetProductByIdAsync(product.Id, _cancellationToken);

        // Assert
        Assert.NotNull(productFound);
        Assert.Equal(product.Id, productFound.Id);
    }
    
    [Fact]
    public async Task GetProductByGuidAsync_ShouldReturnProductByGuid()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);
        
        // Arrange
        var product = new Product { Guid = Guid.NewGuid() };
        await _repository.AddProductAsync(product, _cancellationToken);
        await _dataContext.SaveChangesAsync(_cancellationToken);

        // Act
        var productFound = await _repository
            .GetProductByGuidAsync(product.Guid, _cancellationToken);

        // Assert
        Assert.NotNull(productFound);
        Assert.Equal(product.Guid, productFound.Guid);
    }
    
    [Fact]
    public async Task GetPaginatedProductsByNameAsync_ShouldReturnPaginatedProductsByName()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);
        
        // Arrange
        var product1 = new Product { Name = "Bebida Pepsi 2lts"};
        var product2 = new Product { Name = "Galletas Oreo Paquete 6 unidades"};
        var product3 = new Product { Name = "Bebida Mirinda 2lts"};

        await _repository
            .AddProductAsync(product1, _cancellationToken);

        await _repository
            .AddProductAsync(product2, _cancellationToken);
        
        await _repository
            .AddProductAsync(product3, _cancellationToken);

        await _dataContext.SaveChangesAsync(_cancellationToken);

        // Act
        var createdProducts = await _repository
            .GetProductsByNamePaginated("bebida", _cancellationToken);

        // Assert
        Assert.Equal(2, createdProducts.Count);
    }

    [Fact]
    public async Task GetProductIdByGuid_ShouldReturnProduct()
    {
        await _dataContext.Database.EnsureDeletedAsync(_cancellationToken);

        // Arrange
        var product = new Product { Id = 10, Guid = Guid.NewGuid() };

        await _repository.AddProductAsync(product, _cancellationToken);
        await _dataContext.SaveChangesAsync(_cancellationToken);

        // Act
        var productIdFound = _repository.GetProductIdByGuid(product.Guid);

        // Assert
        Assert.True(productIdFound > 0, "Invalid User ID / User ID not found");
        Assert.Equal(product.Id, productIdFound);
    }

    public void Dispose()
    {
        _dataContext.Dispose();
    }
    
    ~ProductRepositoryIntegrationTests()
    {
        Dispose();
    }
}
