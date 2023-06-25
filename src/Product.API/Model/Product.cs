﻿namespace API.Model;

public class Product : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

