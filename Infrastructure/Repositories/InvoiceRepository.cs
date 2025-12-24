using Application.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


/// <summary>
/// INVOICE REPOSITORY IMPLEMENTATION
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
internal class InvoiceRepository: IInvoiceRepository
{
    private readonly DataContext _context;

    public InvoiceRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task AddInvoice(Invoice invoice, CancellationToken cancellationToken)
    {    
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            await _context.AddAsync(invoice, cancellationToken);
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
        }
    }
    
    public async Task<List<Invoice>> GetInvoicesPaginatedAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return await _context.Invoices
                .Include(invoice => invoice.InvoiceProducts)
                .ThenInclude(record => record.Product)
                .ToListAsync(cancellationToken);
    }
}


/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open source Project
 */
