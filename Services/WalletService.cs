using Microsoft.EntityFrameworkCore;
using VirtualCardAPI.Model;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

public class WalletService
{
    private readonly ApplicationDbContext _context;

    public WalletService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateWallet(string userId)
    {
        var wallet = new Wallet { UserId = userId, Balance = 0 };
        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetBalance(string userId)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        return wallet?.Balance ?? 0;
    }

    public async Task Deposit(string userId, decimal amount)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wallet != null)
        {
            wallet.Balance += amount;

            // Log transaction
            _context.Transactions.Add(new Transaction
            {
                UserId = userId,
                Type = "Deposit",
                Amount = amount
            });

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Withdraw(string userId, decimal amount)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wallet != null && wallet.Balance >= amount)
        {
            wallet.Balance -= amount;

            // Log transaction
            _context.Transactions.Add(new Transaction
            {
                UserId = userId,
                Type = "Withdrawal",
                Amount = amount
            });

            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    private readonly string encryptionKey = "YOUR_SECRET_KEY";

    private string EncryptBalance(decimal balance)
    {
        byte[] data = Encoding.UTF8.GetBytes(balance.ToString());
        using (var sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(data);
            return Convert.ToBase64String(hash);
        }
    }

    public async Task<string> GetEncryptedBalance(string userId)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        return wallet == null ? "0" : EncryptBalance(wallet.Balance);
    }


}
