using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs
{
    public class DataComaparableDTO
    {
        [Required(ErrorMessage = "Initial data is mandatory.")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "An end date is required.")]
        public DateTime DataFim { get; set; }
    }
}