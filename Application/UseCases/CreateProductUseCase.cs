using Business.Interfaces;
using Business.Models;
using Business.UseCases;

namespace Application.UseCases;

/// <summary>
/// 
/// </summary>
public class CreateProductUseCase : ICreateProductUseCase
{

    private IProductService _productService;

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