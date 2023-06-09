using BlaX.CryptoAutoTrading.Domain.Enums;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto
{
    public class AuthorizedUserDto
    {
        public Guid UserId { get; set; }

        public UserRoleEnum UserRole { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public AuthorizedUserDto() { }

        public AuthorizedUserDto(string userRole) => UserRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), userRole ?? "User", true);

        public AuthorizedUserDto(Guid userId, string name, string surname, string email, string userRole) : this(userRole)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
