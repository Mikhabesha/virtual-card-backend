namespace VirtualCardAPI.Model
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public bool Is2FAEnabled { get; set; } = false;
        public string? TwoFactorSecret { get; set; } // Secret Key for Google Authenticator
    }


}
