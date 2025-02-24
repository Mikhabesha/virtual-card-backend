namespace VirtualCardAPI.Model
{
    using System;

    public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; } // "Deposit", "Withdrawal", "CardTopUp"
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
