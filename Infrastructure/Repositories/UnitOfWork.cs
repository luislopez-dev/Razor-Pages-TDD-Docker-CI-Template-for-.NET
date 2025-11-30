using Application.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

/// <summary>
/// UNIT OF WORK PATTERN IMPLEMENTATION
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class UnitOfWork: IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    public IProductRepository ProductRepository => new ProductRepository(_context);
    public IInvoiceRepository InvoiceRepository => new InvoiceRepository(_context);
    public IInvoiceProductRepository InvoiceProductRepository => new InvoiceProductRepository(_context);

    public async Task<bool> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
    public IDbContextTransaction BeginTransaction(CancellationToken cancellationToken)
    {
        return _context.Database.BeginTransaction();
    }
}

/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open source Project
 */
