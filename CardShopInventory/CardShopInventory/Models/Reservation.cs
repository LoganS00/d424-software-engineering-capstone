using System.ComponentModel.DataAnnotations;

namespace CardShopInventory.Models;

public enum ReservationStatus
{
    Pending,
    Fulfilled,
    Cancelled
}

public class Reservation
{
    public int Id { get; set; }

    public int ItemId { get; set; }
    public InventoryItem? Item { get; set; }

    [Required, StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string CustomerContact { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int Quantity { get; set; } = 1;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
}