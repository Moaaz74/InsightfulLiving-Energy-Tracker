﻿using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOS.Home
{
    public class HomeUpdateDto
    {
     //   [Required(ErrorMessage = "The UserId field is required.")]
        public string? UserId { get; set; }

       // [Required(ErrorMessage = "The NumberOfRooms field is required.")]
        public required string? NumberOfRooms { get; set; }

        
    }
}
