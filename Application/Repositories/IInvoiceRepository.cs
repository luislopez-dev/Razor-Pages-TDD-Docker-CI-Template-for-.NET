
using Domain.Entities;

namespace Application.Repositories;

/// <summary>
/// INVOICE REPOSITORY INTERFACE
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public interface IInvoiceRepository
{
    public Task AddInvoice(Invoice invoice, CancellationToken cancellationToken);
    public Task<List<Invoice>> GetInvoicesPaginatedAsync(CancellationToken cancellationToken);
}