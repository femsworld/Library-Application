using System.Text.Json.Serialization;

namespace WebApi.Domain.Entities
{
    public class User : BaseEnity
    {
        public string Name { get; set; }
        public string Email { get; set; } 
        public int Age { get; set; }
        // [Ignore]
        // public byte[]? Password { get; set; } 
        public string Password { get; set; }
        // public Role Role { get; set; } = Role.Client;
        public Role Role { get; set; }
        public string? Avatar { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role {
        Admin,
        Client,
        Librarian
    }
}