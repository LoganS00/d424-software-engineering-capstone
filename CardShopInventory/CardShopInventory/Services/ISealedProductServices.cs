using CardShopInventory.Models;

namespace CardShopInventory.Services;

public interface ISealedProductService
{
    Task<List<SealedProduct>> SearchAsync(string? searchTerm);
    Task<SealedProduct?> GetByIdAsync(int id);
    Task<SealedProduct> CreateAsync(SealedProduct product);
    Task UpdateAsync(SealedProduct product);
    Task DeleteAsync(int id);
}