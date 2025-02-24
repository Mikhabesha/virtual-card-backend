namespace VirtualCardAPI.Model
{
    public class Wallet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; } = 0;

        public ApplicationUser User { get; set; }
    }

}
