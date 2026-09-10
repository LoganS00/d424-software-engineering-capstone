using CardShopInventory.Models;
using Xunit;

namespace CardShopInventory.Tests;

public class InventoryItemTests
{
    [Fact]
    public void Price_NegativeValue_ClampsToZero()
    {
        var card = new Card { Price = -10 };
        Assert.Equal(0, card.Price);
    }

    [Fact]
    public void Quantity_NegativeValue_ClampsToZero()
    {
        var card = new Card { Quantity = -5 };
        Assert.Equal(0, card.Quantity);
    }

    [Fact]
    public void Price_PositiveValue_IsStoredUnchanged()
    {
        var card = new Card { Price = 19.99m };
        Assert.Equal(19.99m, card.Price);
    }

    [Fact]
    public void Card_GetDetails_ReturnsExpectedFormat()
    {
        var card = new Card
        {
            Name = "Charizard",
            SetName = "Base Set",
            CardNumber = "4/102",
            Condition = "Near Mint"
        };

        var result = card.GetDetails();

        Assert.Equal("Charizard (Base Set #4/102) — Near Mint", result);
    }

    [Fact]
    public void SealedProduct_GetDetails_ReturnsExpectedFormat()
    {
        var product = new SealedProduct
        {
            Name = "Booster Box",
            Manufacturer = "The Pokémon Company",
            ProductType = "Booster Box",
            UnitsPerPackage = 36
        };

        var result = product.GetDetails();

        Assert.Equal("Booster Box (The Pokémon Company Booster Box, 36 units)", result);
    }

    [Fact]
    public void GetDetails_IsPolymorphic_AcrossInventoryItemReferences()
    {
        InventoryItem card = new Card { Name = "Pikachu", SetName = "Jungle", CardNumber = "60/64" };
        InventoryItem product = new SealedProduct { Name = "Elite Trainer Box", Manufacturer = "Pokémon", ProductType = "ETB", UnitsPerPackage = 1 };

        Assert.Contains("Jungle", card.GetDetails());
        Assert.Contains("Pokémon", product.GetDetails());
        Assert.NotEqual(card.GetDetails(), product.GetDetails());
    }
}