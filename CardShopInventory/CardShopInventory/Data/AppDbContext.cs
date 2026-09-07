using Microsoft.EntityFrameworkCore;
using CardShopInventory.Models;

namespace CardShopInventory.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Card> Cards => Set<Card>();
}