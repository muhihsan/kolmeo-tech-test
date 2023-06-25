using API.Model;

namespace API.Dto;

/// <summary>
/// One of the benefit using this Dto is to prevent accidentally exposing sensitive information
/// </summary>
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public ProductDto()
    {
    }

    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
    }
}

