using System.ComponentModel.DataAnnotations;
using EstocariaNet.Models;

namespace EstocariaNet.Shared.DTOs.Creates
{
    public class CreateEstoquistaDTO
    {
        [Required(ErrorMessage = "A valid user id is required")]
        public string? AplicationUserId { get; set; }

        [Required(ErrorMessage = "a valid CPF id is required")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "A valid Phone number id is required")]
        public string? Celular { get; set; }
        public DateTime? DataInicio { get; set; }
         public int? EstoqueId { get; set; }
        public CreateEstoquistaDTO() { DataInicio = DateTime.Now; }
    }
}