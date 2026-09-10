using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CardShopInventory.Data;
using CardShopInventory.Models;

namespace CardShopInventory.Services;

public class AdminAccountService : IAdminAccountService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    private readonly PasswordHasher<AdminAccount> _hasher = new();

    public AdminAccountService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> AdminExistsAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.AdminAccounts.AnyAsync();
    }

    public async Task CreateAdminAsync(string username, string password)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var account = new AdminAccount { Username = username.Trim() };
        account.PasswordHash = _hasher.HashPassword(account, password);

        context.AdminAccounts.Add(account);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var account = await context.AdminAccounts
            .FirstOrDefaultAsync(a => a.Username == username.Trim());

        if (account is null) return false;

        var result = _hasher.VerifyHashedPassword(account, account.PasswordHash, password);
        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}