using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Presentation.Controllers;

namespace Presentation.Tests.Controllers;

/// <summary>
/// UNIT TEST responsible for testing PRODUCTS CONTROLLER
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev  
/// </remarks>
public class ProductsControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private ProductsController _controller;
    private readonly CancellationToken _token;

    public ProductsControllerTests()
    {
        // Arrange
        _mockService = new Mock<IProductService>();
        _controller = new ProductsController(_mockService.Object)
        {
            TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
        };
        _token = new CancellationToken();
    }

    [Fact]
    public void Create_ActionExecutes_ReturnsViewForCreate()
    {
        // Act
        var result = _controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async void Index_ActionExecutes_ReturnsViewForIndex()
    {
        // Act
        var result = await _controller.Index(_token);

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async void Index_ActionExecutes_ReturnsPaginatedProducts()
    {
        // Arrange
        var products = new List<Product>() { new Product(), new Product() };

        _mockService.Setup(service => service
                .GetProductsPaginatedAsync(_token))
            .Returns(Task.FromResult(products));

        // Act
        var result = await _controller.Index(_token);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        var productsReturned = Assert.IsType<List<Product>>(viewResult.Model);

        Assert.Equal(2, productsReturned.Count);
    }

    [Fact]
    public async void Search_ActionExecutes_ReturnsViewForSearch()
    {
        // Arrange
        var productName = "chocolate";

        // Act
        var result = await _controller.Search(productName, _token);

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async void Search_ActionExecutes_ReturnsPaginatedProductsByName()
    {
        // Arrange
        var productName = "chocolate";
        var products = new List<Product>() { new Product(), new Product() };
    
        _mockService.Setup(service => service
                .GetProductsByNamePaginatedAsync(productName, _token))
            .Returns(Task.FromResult(products));
        // Act

        var result = await _controller.Search(productName, _token);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        var productsReturned = Assert.IsType<List<Product>>(viewResult.Model);

        Assert.Equal(2, productsReturned.Count);
    }

    [Fact]
    public async void Details_ActionExecutes_RedirectsToNotFoundAction()
    {
        // Arrange
        var guid = new Guid();

        // Act
        var result = await _controller.Details(guid, _token);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async void Details_ActionExecutes_ReturnsViewForDetails()
    {
        // Arrange 
        var guid = new Guid();
        var product = new Product { Guid = guid };

        _mockService.Setup(service =>
                service.GetProductByGuidAsync(guid, _token))
            .Returns(Task.FromResult(product));
        
        // Act
        
        var result = await _controller.Details(guid, _token);

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async void Edit_ActionExecutes_RedirectsToNotFoundAction()
    {
        // Arrange
        var guid = new Guid();

        // Act
        var result = await _controller.Edit(guid, _token);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async void Edit_ActionExecutes_ReturnsViewForEdit()
    {
        // Arrange
        var guid = new Guid();
        var product = new Product { Guid = guid };

        _mockService.Setup(service =>
                service.GetProductByGuidAsync(guid, _token))
            .Returns(Task.FromResult(product));
        
        // Act
        var result = await _controller.Edit(guid, _token);

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_ProductDeletedSuccessfully_ReturnsRedirectToIndex()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var product = new Product { Guid = guid };

        _mockService.Setup(service => service
                .DeleteProductByGuidAsync(guid, _token))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteConfirmed(product, _token);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal("¡Producto eliminado exitosamente!", _controller.TempData["message"]);

        _mockService.Verify(service => service
            .DeleteProductByGuidAsync(guid, _token), Times.Once);
    }

    [Fact]
    public async Task Edit_ProductEditedSuccessfully_ReturnsRedirectToDetails()
    {
        // Arrange
        var guid = new Guid();
        var product = new Product{ Guid = guid };

        _mockService.Setup(service => service
                .UpdateProduct(product, _token))
            .Returns(Task.CompletedTask);

        // Act
        var result = _controller.Edit(product, _token);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirectToActionResult.ActionName);
        Assert.Equal("!Producto actualizado exitosamente!", _controller.TempData["message"]);
        
        _mockService.Verify(service => service
            .UpdateProduct(product, _token), Times.Once);
    }

    [Fact]
    public async Task Create_ProductCreatedSuccessfully_ReturnsRedirectionToIndex()
    {
        // Arrange
        var product = new Product();

        _mockService.Setup(service => service.AddProductAsync(product, _token))
            .Returns(Task.CompletedTask);
        
        // Act
        var result = await _controller.Create(product, _token);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal("¡Producto creado exitosamente!", _controller.TempData["message"]);
        
    }
}