namespace VirtualCardAPI.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using VirtualCardAPI.Model;

    public class CardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Card> CreateCard(string userId)
        {
            var card = new Card
            {
                UserId = userId,
                CardNumber = GenerateCardNumber(),
                ExpiryDate = DateTime.UtcNow.AddYears(3).ToString("MM/yy"),
                CVV = new Random().Next(100, 999).ToString(),
                Balance = 0,
                IsActive = true
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> GetUserCard(string userId)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        private string GenerateCardNumber()
        {
            Random random = new Random();
            return string.Concat(Enumerable.Range(0, 16).Select(_ => random.Next(0, 10).ToString()));
        }

        public async Task<bool> TopUpCard(string userId, decimal amount)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.UserId == userId);

            if (wallet == null || card == null || !card.IsActive) return false;
            if (wallet.Balance < amount) return false;

            wallet.Balance -= amount;
            card.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                UserId = userId,
                Type = "CardTopUp",
                Amount = amount
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateCard(string userId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.UserId == userId);
            if (card == null) return false;

            card.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactivateCard(string userId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.UserId == userId);
            if (card == null) return false;

            card.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }



    }

}
