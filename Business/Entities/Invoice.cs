using Business.Entities;

namespace Business.Models;

/// <summary>
/// Invoice Model
/// </summary>
/// <remarks>
/// Author: Luis López  
/// GitHub: https://github.com/luislopez-dev
/// Description: Open source project: Enterprise-.Net-Architecture-Template
/// </remarks>
public class Invoice
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string ClientName { get; set; }
    public string ClientAddress { get; set; }
    public string ClientNit { get; set; }
    public float Discount { get; set; } = 2;
    public float Total { get; set; } = 100;
    public string PaymentMethod { get; set; }
    
    public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
}

/*
 ** Author: Luis René López
 ** Website: https://github.com/luislopez-dev
 ** Description: Open source Project
 */
