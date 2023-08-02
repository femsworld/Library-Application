namespace WebApi.Domain.Entities
{
    // [AutoMap(typeof(UserDto))]
    public class User : BaseEnity
    {
        public string? Name { get; set; }
        public string? Email { get; set; } 
        // [Ignore]
        // public byte[] Password { get; set; } 
        public string Password { get; set; }
        public Address Address { get; set; } 
        public Role Role { get; set; } = Role.Client;
    }

    // internal class UserDto
    // {
    //    public string? Name { get; set; }
    //     public string? Email { get; set; }
    //     // public byte[] Password { get; set; }  
    //     public string? Password { get; set; }  
    // }

    public enum Role {
        Admin,
        Client,
        Librarian
    }
}