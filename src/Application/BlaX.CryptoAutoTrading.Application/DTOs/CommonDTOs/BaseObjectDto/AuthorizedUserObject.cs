using BlaX.CryptoAutoTrading.Domain.Enums;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto
{
    public class AuthorizedUserObject
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public UserRoleEnum UserRole { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Surname { get; set; }

        [JsonIgnore]
        public string Email { get; set; }

        public AuthorizedUserObject() { }

        public AuthorizedUserObject(string userRole) => UserRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), userRole ?? "User", true);

        public AuthorizedUserObject(Guid userId, string name, string surname, string email, string userRole) : this(userRole)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
