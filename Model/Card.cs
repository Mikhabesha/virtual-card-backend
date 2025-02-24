namespace VirtualCardAPI.Model
{
    public class Card
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CardNumber { get; set; } // Placeholder for now
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal Balance { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        public ApplicationUser User { get; set; }
    }

}
