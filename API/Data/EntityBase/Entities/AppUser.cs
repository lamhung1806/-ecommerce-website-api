using Microsoft.AspNetCore.Identity;

namespace API.Data.EntityBase.Entities
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        public Cart Cart { get; set; }
    }
}
