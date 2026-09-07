using Microsoft.EntityFrameworkCore;
using CardShopInventory.Data;
using CardShopInventory.Models;

namespace CardShopInventory.Services;

public class CardService : ICardService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public CardService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Card>> SearchAsync(string? searchTerm)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var query = context.Cards.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(c =>
                EF.Functions.Like(c.Name, $"%{term}%") ||
                EF.Functions.Like(c.SetName, $"%{term}%") ||
                EF.Functions.Like(c.CardNumber, $"%{term}%"));
        }

        return await query.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Cards.FindAsync(id);
    }

    public async Task<Card> CreateAsync(Card card)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.Cards.Add(card);
        await context.SaveChangesAsync();
        return card;
    }

    public async Task UpdateAsync(Card card)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.Cards.Update(card);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var card = await context.Cards.FindAsync(id);
        if (card is null) return;

        if (!string.IsNullOrEmpty(card.ImagePath))
        {
            var fullPath = Path.Combine("wwwroot", card.ImagePath.TrimStart('/'));
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }

        context.Cards.Remove(card);
        await context.SaveChangesAsync();
    }
}