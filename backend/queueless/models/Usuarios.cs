using System.Runtime.CompilerServices;

namespace queueless.Models
{
    public class Usuarios
    {
        public Usuarios(String name, String username, String password, UserRole role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Username = username;
            Password = password;
            Role = role;
            CreatingDT = DateTime.Now;
            IsActive = true;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public enum UserRole
        {
            Admin,
            User
        }
        public UserRole Role { get; set; } = UserRole.User;
        public Boolean IsActive { get; set; }
        public DateTime CreatingDT { get; set; }
    }
}