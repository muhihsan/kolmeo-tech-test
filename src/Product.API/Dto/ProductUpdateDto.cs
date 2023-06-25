namespace API.Dto;

public class ProductUpdateDto
{
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }

    public ProductUpdateDto(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}

