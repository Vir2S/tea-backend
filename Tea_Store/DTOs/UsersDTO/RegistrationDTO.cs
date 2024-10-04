﻿using System.ComponentModel.DataAnnotations;

namespace Tea_Store.DTOs.UsersDTO
{
    public class RegistrationDTO
    {
        [Required]
        public string FName { get; set; } = null!;

        [Required]
        public string SName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!;

        [Range(0, 150)]
        public int Age { get; set; }
    }
}