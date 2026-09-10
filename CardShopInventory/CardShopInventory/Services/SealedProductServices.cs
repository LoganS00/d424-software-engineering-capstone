using Microsoft.EntityFrameworkCore;
using CardShopInventory.Data;
using CardShopInventory.Models;

namespace CardShopInventory.Services;

public class SealedProductService : ISealedProductService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public SealedProductService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<SealedProduct>> SearchAsync(string? searchTerm)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var query = context.SealedProducts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(p =>
                EF.Functions.Like(p.Name, $"%{term}%") ||
                EF.Functions.Like(p.Manufacturer, $"%{term}%") ||
                EF.Functions.Like(p.ProductType, $"%{term}%"));
        }

        return await query.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<SealedProduct?> GetByIdAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.SealedProducts.FindAsync(id);
    }

    public async Task<SealedProduct> CreateAsync(SealedProduct product)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.SealedProducts.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(SealedProduct product)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.SealedProducts.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var product = await context.SealedProducts.FindAsync(id);
        if (product is null) return;

        if (!string.IsNullOrEmpty(product.ImagePath))
        {
            var fullPath = Path.Combine("wwwroot", product.ImagePath.TrimStart('/'));
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }

        context.SealedProducts.Remove(product);
        await context.SaveChangesAsync();
    }
}