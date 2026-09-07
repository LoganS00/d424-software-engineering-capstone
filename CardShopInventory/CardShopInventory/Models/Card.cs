using System.ComponentModel.DataAnnotations;

namespace CardShopInventory.Models;

public class Card
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [StringLength(80)]
    public string SetName { get; set; } = string.Empty;

    [StringLength(20)]
    public string CardNumber { get; set; } = string.Empty;

    [StringLength(30)]
    public string Condition { get; set; } = string.Empty;

    [Range(0, 100000)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public string? ImagePath { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;

}