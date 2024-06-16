﻿using System.ComponentModel.DataAnnotations;

namespace EstocariaNet.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="User name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
