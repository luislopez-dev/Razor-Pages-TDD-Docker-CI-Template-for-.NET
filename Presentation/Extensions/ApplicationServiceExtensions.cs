/*
 * Author: Luis René López
 * Website: https://github.com/luislopez-dev
 * Description: Open source Project
 */

using Application.Services;
using Application.UseCases;
using Business.Interfaces;
using Business.Models;
using Business.UseCases;
using Business.Validations;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("app"));
            }   
        );        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IValidator<Product>, ProductValidator>();
        services.AddScoped<IValidator<Invoice>, InvoiceValidator>();
        
        return services;
    }
}

/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open source Project
 */
