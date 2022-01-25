using Microsoft.AspNetCore.Identity;

namespace API_Examen.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set;} = default!;
    }
}
