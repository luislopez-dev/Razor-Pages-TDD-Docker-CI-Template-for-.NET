using Application.Repositories;
using Business.Exceptions.Invoice.Exceptions;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// CONTROLLER FOR INVOICES
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class InvoicesController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoiceService _invoiceService;
    private readonly IProductService _productService;
    private readonly ILogger<InvoicesController> _logger;
    
    public InvoicesController(IUnitOfWork unitOfWork, IInvoiceService invoiceService, IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _invoiceService = invoiceService;
        _productService = productService;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var invoices = await _invoiceService
        .GetInvoicesPaginatedAsync(cancellationToken);
        
        return View(invoices);
    }
    
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        ViewBag.products = await _productService
            .GetProductsPaginatedAsync(cancellationToken);
        
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClientName", "ClientNit", "PaymentMethod", "ClientAddress")] Invoice invoice, 
        int[] selectedProducts, 
        CancellationToken cancellationToken)
    {
        try
        {
            await _invoiceService
                .AddInvoiceAsync(invoice, selectedProducts, cancellationToken);

            TempData["message"] = "!Factura creada exitosamente!";
            
            return RedirectToAction(nameof(Index));   
        }
        catch (InvoiceValidationException e)
        {
            Console.WriteLine("Error in validations");
            foreach (var error in e.ValidationFailures)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            
            ViewBag.products = await _productService
                .GetProductsPaginatedAsync(cancellationToken);
            
            return View(invoice);
        }
        catch (CreateInvoiceException e)
        {
            TempData["message"] = "¡No se pudo crear la factura, intentelo de nuevo más tarde!";
            Console.WriteLine("Error in CreateInvoice");
            return View(invoice);
        }
    }
}