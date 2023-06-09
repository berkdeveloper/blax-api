using System.ComponentModel.DataAnnotations;

namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto
{
    public class BaseRequestById
    {
        [Required]
        public Guid Id { get; set; }
    }
}
