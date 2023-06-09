using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes
{
    public class BaseRequest
    {
        [JsonIgnore]
        public AuthorizedUserObject AuthorizedUserObject { get; set; }

        public BaseRequest() { }
        public BaseRequest(AuthorizedUserObject authorizedUser) => AuthorizedUserObject = authorizedUser;
    }
}
