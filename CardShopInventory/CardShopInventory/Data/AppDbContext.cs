using Microsoft.EntityFrameworkCore;
using CardShopInventory.Models;

namespace CardShopInventory.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<SealedProduct> SealedProducts => Set<SealedProduct>();
    public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
}