﻿using Microsoft.AspNetCore.Components.Forms;

public class ItemViewModel
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }  // For displaying the image
    
}
