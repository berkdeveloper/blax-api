using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes
{
    public class BaseRequestDto
    {
        public AuthorizedUserDto AuthorizedUser { get; set; }

        public BaseRequestDto() { }
        public BaseRequestDto(AuthorizedUserDto authorizedUserDto) => AuthorizedUser = authorizedUserDto;
    }
}
