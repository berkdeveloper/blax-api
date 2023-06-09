using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes;

namespace BlaX.CryptoAutoTrading.Application.DTOs
{
    public class TestDto : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string FaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EmailNumber { get; set; }
    }
}
