using CardShopInventory.Models;

namespace CardShopInventory.Services;

public interface ICardService
{
    Task<List<Card>> SearchAsync(string? searchTerm);
    Task<Card?> GetByIdAsync(int id);
    Task<Card> CreateAsync(Card card);
    Task UpdateAsync(Card card);
    Task DeleteAsync(int id);
}