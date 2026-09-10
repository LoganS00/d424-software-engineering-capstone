using Microsoft.EntityFrameworkCore;
using CardShopInventory.Data;
using CardShopInventory.Models;

namespace CardShopInventory.Services;

public class ReservationService : IReservationService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ReservationService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Reservation> CreateAsync(int cardId, string customerName, string customerContact, int quantity)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var reservation = new Reservation
        {
            CardId = cardId,
            CustomerName = customerName.Trim(),
            CustomerContact = customerContact.Trim(),
            Quantity = quantity
        };

        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Reservations
            .Include(r => r.Card)
            .OrderByDescending(r => r.RequestedAt)
            .ToListAsync();
    }

    public async Task UpdateStatusAsync(int id, ReservationStatus status)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var reservation = await context.Reservations.FindAsync(id);
        if (reservation is null) return;

        reservation.Status = status;
        await context.SaveChangesAsync();
    }
}