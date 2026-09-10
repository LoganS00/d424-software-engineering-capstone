using System.ComponentModel.DataAnnotations;

namespace CardShopInventory.Models;

public abstract class InventoryItem
{
    private decimal _price;
    private int _quantity;

    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 100000)]
    public decimal Price
    {
        get => _price;
        set => _price = value < 0 ? 0 : value;
    }

    [Range(0, int.MaxValue)]
    public int Quantity
    {
        get => _quantity;
        set => _quantity = value < 0 ? 0 : value;
    }

    public string? ImagePath { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    public abstract string GetDetails();
}