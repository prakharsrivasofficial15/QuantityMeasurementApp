using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementAPI.DTOs
{
    public class CompareRequest
    {
        [Required]
        public QuantityInputDTO Quantity1 { get; set; } = new();
        
        [Required]
        public QuantityInputDTO Quantity2 { get; set; } = new();
    }
}