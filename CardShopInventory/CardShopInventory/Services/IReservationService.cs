using CardShopInventory.Models;

namespace CardShopInventory.Services;

public interface IReservationService
{
    Task<Reservation> CreateAsync(int cardId, string customerName, string customerContact, int quantity);
    Task<List<Reservation>> GetAllAsync();
    Task UpdateStatusAsync(int id, ReservationStatus status);
}