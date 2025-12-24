
using Application.Exceptions.Product.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// CONTROLLER FOR PRODUCTS
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class ProductsController : BaseController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public ActionResult Create()
    {
        return View();
    }
    
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
       
        var products = await _productService
                .GetProductsPaginatedAsync(cancellationToken);
        return View(products);
    }

    public async Task<IActionResult> Search(string productName, CancellationToken cancellationToken)
    {
        var products = await
            _productService
                .GetProductsByNamePaginatedAsync(productName, cancellationToken);
        
        return View("Index", products);
    }
    
    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await 
                _productService
                    .GetProductByGuidAsync(id, cancellationToken);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(product);
        }
        catch (GetProductException)
        {
            return RedirectToAction("NotFound", "Error");
        }
    }
    
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await 
                _productService
                    .GetProductByGuidAsync(id, cancellationToken);
        
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(product);
        }
        catch (GetProductException e)
        {
            return RedirectToAction("NotFound", "Error");
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([Bind("Guid")]Product product, CancellationToken cancellationToken)
    {
        try
        {
            await _productService
                .DeleteProductByGuidAsync(product.Guid, cancellationToken);
            
            TempData["message"] = "¡Producto eliminado exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        catch (DeleteProductException e)
        {
            TempData["message"] = "¡No se pudo eliminar el producto, inténtelo más tarde!";
            
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product, CancellationToken cancellationToken)
    {
        try
        {
            _productService
                .UpdateProduct(product, cancellationToken);

            TempData["message"] = "!Producto actualizado exitosamente!";
            
            return RedirectToAction(nameof(Details), new { id = product.Guid});
        }
        catch (UpdateProductException e)
        {
            TempData["message"] = "!No es posible actualizar el producto en este momento!";
        }
        return RedirectToAction(nameof(Details), new {product.Guid, cancellationToken});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name, Price, Stock, Description")]Product product, CancellationToken cancellationToken)
    {
        try
        {
            await _productService
                .AddProductAsync(product, cancellationToken);

            TempData["message"] = "¡Producto creado exitosamente!";
            
            return RedirectToAction(nameof(Index));
        }
        catch (ProductValidationException e)
        {
            foreach (var error in e.ValidationFailures)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(product);
        }
        catch (CreateProductException e)
        {
            TempData["message"] = "¡No es posible crear el producto en este momento!";
            
            return View(product);
        }
    }
}