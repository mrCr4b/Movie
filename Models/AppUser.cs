using Microsoft.AspNetCore.Identity;

namespace Movie.Models
{
    public class AppUser : IdentityUser
    {
        public int? Savings { get; set; }
    }
}
