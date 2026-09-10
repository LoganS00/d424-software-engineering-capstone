namespace CardShopInventory.Services;

public interface IAdminAccountService
{
    Task<bool> AdminExistsAsync();
    Task CreateAdminAsync(string username, string password);
    Task<bool> ValidateCredentialsAsync(string username, string password);
}