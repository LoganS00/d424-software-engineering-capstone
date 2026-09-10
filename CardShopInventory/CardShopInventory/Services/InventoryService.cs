using Microsoft.EntityFrameworkCore;
using CardShopInventory.Data;
using CardShopInventory.Models;

namespace CardShopInventory.Services;

public class InventoryService : IInventoryService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public InventoryService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<InventoryItem>> SearchAllAsync(string? searchTerm)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        IQueryable<Card> cardQuery = context.Cards;
        IQueryable<SealedProduct> productQuery = context.SealedProducts;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();

            cardQuery = cardQuery.Where(c =>
                EF.Functions.Like(c.Name, $"%{term}%") ||
                EF.Functions.Like(c.SetName, $"%{term}%") ||
                EF.Functions.Like(c.CardNumber, $"%{term}%"));

            productQuery = productQuery.Where(p =>
                EF.Functions.Like(p.Name, $"%{term}%") ||
                EF.Functions.Like(p.Manufacturer, $"%{term}%") ||
                EF.Functions.Like(p.ProductType, $"%{term}%"));
        }

        var cards = await cardQuery.ToListAsync();
        var products = await productQuery.ToListAsync();

        return cards.Cast<InventoryItem>()
            .Concat(products.Cast<InventoryItem>())
            .OrderBy(i => i.Name)
            .ToList();
    }
}