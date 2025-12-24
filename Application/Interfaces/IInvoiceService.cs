using Domain.Entities;

namespace Application.Interfaces;

/// <summary>
/// INVOICE SERVICE INTERFACE
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public interface IInvoiceService
{
    public Task AddInvoiceAsync(Invoice invoice, int[] selectedProducts, CancellationToken cancellationToken);
    public Task<List<Invoice>> GetInvoicesPaginatedAsync(CancellationToken cancellationToken);
}