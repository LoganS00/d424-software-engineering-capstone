using System.ComponentModel.DataAnnotations;

namespace CardShopInventory.Models;

public class Card : InventoryItem
{
    [StringLength(80)]
    public string SetName { get; set; } = string.Empty;

    [StringLength(20)]
    public string CardNumber { get; set; } = string.Empty;

    [StringLength(30)]
    public string Condition { get; set; } = "Near Mint";

    public override string GetDetails() =>
        $"{Name} ({SetName} #{CardNumber}) — {Condition}";
}