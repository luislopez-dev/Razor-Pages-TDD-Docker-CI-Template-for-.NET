using Business.Entities;
using Business.Models;
using Business.Services;
using Business.UseCases;

namespace Application.UseCases;

/// <summary>
/// Use case responsible for creating a new product
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev  
/// </remarks>
public class CreateProductUseCase : ICreateProductUseCase
{

    private IProductService _productService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productService"></param>
    public CreateProductUseCase(IProductService productService)
    {
        this._productService = productService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task InvokeAsync(Product product, CancellationToken cancellationToken)
    {
        await _productService.AddProductAsync(product, cancellationToken);
    }
}