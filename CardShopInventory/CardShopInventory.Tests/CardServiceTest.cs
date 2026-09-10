using CardShopInventory.Models;
using CardShopInventory.Services;
using Xunit;

namespace CardShopInventory.Tests;

public class CardServiceTests : IDisposable
{
    private readonly TestDbContextFactory _factory;
    private readonly CardService _service;

    public CardServiceTests()
    {
        _factory = new TestDbContextFactory();
        _service = new CardService(_factory);
    }

    [Fact]
    public async Task CreateAsync_AddsCard_ThatCanBeRetrieved()
    {
        var card = new Card { Name = "Blastoise", SetName = "Base Set", CardNumber = "2/102", Price = 25.00m, Quantity = 3 };

        await _service.CreateAsync(card);
        var results = await _service.SearchAsync(null);

        Assert.Single(results);
        Assert.Equal("Blastoise", results[0].Name);
    }

    [Fact]
    public async Task SearchAsync_MatchesPartialName()
    {
        await _service.CreateAsync(new Card { Name = "Charizard", SetName = "Base Set", CardNumber = "4/102" });
        await _service.CreateAsync(new Card { Name = "Blastoise", SetName = "Base Set", CardNumber = "2/102" });

        var results = await _service.SearchAsync("char");

        Assert.Single(results);
        Assert.Equal("Charizard", results[0].Name);
    }

    [Fact]
    public async Task UpdateAsync_PersistsChangedQuantity()
    {
        var card = new Card { Name = "Venusaur", SetName = "Base Set", CardNumber = "15/102", Quantity = 1 };
        await _service.CreateAsync(card);

        card.Quantity = 10;
        await _service.UpdateAsync(card);

        var updated = await _service.GetByIdAsync(card.Id);
        Assert.Equal(10, updated!.Quantity);
    }

    [Fact]
    public async Task DeleteAsync_RemovesCard()
    {
        var card = new Card { Name = "Alakazam", SetName = "Base Set", CardNumber = "1/102" };
        await _service.CreateAsync(card);

        await _service.DeleteAsync(card.Id);
        var results = await _service.SearchAsync(null);

        Assert.Empty(results);
    }

    public void Dispose() => _factory.Dispose();
}