using CardShopInventory.Models;

namespace CardShopInventory.Services;

public interface IInventoryService
{
    Task<List<InventoryItem>> SearchAllAsync(string? searchTerm);
    Task<InventoryItem?> GetByIdAsync(int id);
}