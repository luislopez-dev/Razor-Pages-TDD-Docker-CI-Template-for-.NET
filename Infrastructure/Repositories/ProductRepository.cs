using Application.Repositories;
using Business.Entities;
using Business.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// PRODUCT REPOSITORY IMPLEMENTATION
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class ProductRepository: IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _context.AddAsync(product, cancellationToken);
        }
        catch (DbUpdateException e)
        {
        }
    }
    public async Task DeleteProductByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Guid == guid, cancellationToken);
            _context.Remove(product);
        }
        catch (DbUpdateException e)
        {
        }
    }
    public void UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        _context.Entry(product).State = EntityState.Modified;
    }
    public async Task<List<Product>> GetProductsPaginatedAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var products = from n in _context.Products select n;
        
        return await products.ToListAsync(cancellationToken);
    }
    public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        
        return await _context
                .Products
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<Product> GetProductByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {

        return await _context
            .Products
            .FirstOrDefaultAsync(m => m.Guid == guid, cancellationToken);
    }

    public async Task<List<Product>> GetProductsByNamePaginated(string name, CancellationToken cancellationToken)
    {

        return await _context
            .Products
            .Where(p => EF.Functions
                .Like(p.Name, $"%{name}%"))
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }

    public int GetProductIdByGuid(Guid guid)
    {
       return _context.Products
            .Where(p => p.Guid == guid)
            .Select(p => p.Id)
            .FirstOrDefault();
    }
}