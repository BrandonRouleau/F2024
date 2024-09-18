using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using brH60Store.Validation;

namespace brH60Store.Models;

public partial class Product
{
    [Required]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public int ProdCatId { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string? Manufacturer { get; set; }

    [Required]
    [Range(int.MinValue, int.MaxValue, ErrorMessage = "Stock must be a non-negative number")]
    public int Stock { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Buy Price must be a positive number")]
    public decimal? BuyPrice { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Buy Price must be a positive number")]
    public decimal? SellPrice { get; set; }

    public string? Image { get; set; } = null;

    public virtual ProductCategory ProdCat { get; set; } = null!;

    public bool updateStock(int stock) {
        ModelValidator validator = new ModelValidator();
        if (validator.ValidateStock(this.Stock, stock)) {
            this.Stock += stock;
            return true;
        }
        return false;
    }
}
