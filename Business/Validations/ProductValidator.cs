using Business.Entities;
using Business.Models;
using FluentValidation;

namespace Business.Validations;

/// <summary>
/// VALIDATION CLASS FOR PRODUCTS
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class ProductValidator: AbstractValidator<Product> {
    
    public ProductValidator()
    {
        // Validations for Name attribute
        RuleFor(product => product.Name)
            .NotNull().WithMessage("¡El nombre del producto no debe estar vacio!")
            .Length(5, 60).WithMessage("El nombre del producto debe tener entre 5 a 60 caracteres");
        
        // Validations for Price attribute
        RuleFor(product => product.Price)
            .NotNull().WithMessage("¡El precio del producto no debe estar vacio!")
            .InclusiveBetween(1, 100000).WithMessage("El precio del producto debe estar entre 1 y 100,000 quetzales");
        
        // Validations for Description attribute
        RuleFor(product => product.Description)
            .NotNull().WithMessage("¡La descripción del producto no debe estar vacia!")
            .Length(5, 900).WithMessage("La descripción del producto debe tener entre 5 a 900 caracteres");            
            
        // Validations for Stock attribute
        RuleFor(product => product.Stock)
            .NotNull().WithMessage("¡El stock del producto no debe estar vacio!")
            .InclusiveBetween(1, 1000000).WithMessage("El stock del producto debe estar entre una a un millón de unidades");
    }
    
}

/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open source Project
 */
