namespace API.Dto;

public class ProductUpdateDto
{
    public string Name { get; }
    public string Description { get; }
    public int Price { get; }

    public ProductUpdateDto(string name, string description, int price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}

