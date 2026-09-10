using System.ComponentModel.DataAnnotations;

namespace CardShopInventory.Models;

public class SealedProduct : InventoryItem
{
    [StringLength(80)]
    public string Manufacturer { get; set; } = string.Empty;

    [StringLength(50)]
    public string ProductType { get; set; } = "Booster Pack";

    [Range(1, 10000)]
    public int UnitsPerPackage { get; set; } = 1;

    public override string GetDetails() =>
        $"{Name} ({Manufacturer} {ProductType}, {UnitsPerPackage} units)";
}