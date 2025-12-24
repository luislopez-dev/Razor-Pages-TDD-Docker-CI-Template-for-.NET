using Application.Exceptions.Product.Exceptions;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services;


/// <summary>
/// SERVICE CLASS POR PRODUCTS
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class ProductService: IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Product> _validator;
    
    public ProductService(IUnitOfWork unitOfWork, IValidator<Product> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task AddProductAsync(Product product,CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        // Start validations
        ValidationResult validation = await _validator
            .ValidateAsync(product, cancellationToken);

        if (!validation.IsValid)
            throw new ProductValidationException(validation.Errors);
        
        // Start DB operations
        await _unitOfWork
            .ProductRepository
            .AddProductAsync(product, cancellationToken);

        if (!await _unitOfWork.CompleteAsync(cancellationToken))
            throw new CreateProductException();
    }
    public async Task DeleteProductByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
  
        await _unitOfWork
            .ProductRepository
            .DeleteProductByGuidAsync(guid, cancellationToken);
        
        if (!await _unitOfWork.CompleteAsync(cancellationToken))
            throw new DeleteProductException();
    }
    public async Task UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ValidationResult validation = await _validator
            .ValidateAsync(product, cancellationToken);

        if (!validation.IsValid)
            throw new ProductValidationException(validation.Errors);

        product.Id = _unitOfWork
            .ProductRepository
            .GetProductIdByGuid(product.Guid);

        _unitOfWork
            .ProductRepository
            .UpdateProduct(product, cancellationToken);

        if (!await _unitOfWork.CompleteAsync(cancellationToken))
            throw new UpdateProductException();
        
    }
    public async Task<List<Product>> GetProductsPaginatedAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        
        return await _unitOfWork
            .ProductRepository
            .GetProductsPaginatedAsync(cancellationToken);
        
    }
    public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        
        return await _unitOfWork
            .ProductRepository
            .GetProductByIdAsync(id, cancellationToken);
        
    }
    public async Task<Product> GetProductByGuidAsync(Guid guid,CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return await _unitOfWork
                .ProductRepository
                .GetProductByGuidAsync(guid, cancellationToken);
        
    }
    public async Task<List<Product>> GetProductsByNamePaginatedAsync(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return await _unitOfWork
            .ProductRepository
            .GetProductsByNamePaginated(name, cancellationToken);
    }
}