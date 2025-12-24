namespace Domain.Entities;

/// <summary>
/// Product Model
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class Product
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }

    public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
}