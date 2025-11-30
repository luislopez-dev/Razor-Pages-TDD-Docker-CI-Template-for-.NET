using Business.Entities;
using Business.Models;

namespace Business.UseCases;

/// <summary>
/// INTERFACE FOR CREATE PRODUCT USE CASE
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public interface ICreateProductUseCase
{
    Task InvokeAsync(Product product, CancellationToken cancellationToken);
}