using Microsoft.AspNetCore.Identity;

namespace AutoParts.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? NomeCompleto { get; set; }
    }
}
