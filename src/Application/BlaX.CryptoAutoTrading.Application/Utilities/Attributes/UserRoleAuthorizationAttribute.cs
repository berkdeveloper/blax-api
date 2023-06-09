using BlaX.CryptoAutoTrading.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Attributes
{
    public class UserRoleAuthorizationAttribute : AuthorizeAttribute
    {
        public UserRoleAuthorizationAttribute(params UserRoleEnum[] roles) => Roles = string.Join(",", roles);
    }
}
