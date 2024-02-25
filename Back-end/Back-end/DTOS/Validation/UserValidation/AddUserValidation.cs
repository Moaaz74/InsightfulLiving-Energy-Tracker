using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOs
{
    public static class AddUserValidation
    {
        public static class Validator
        {
            public static ValidationResult Validate(AddUserDto dto)
            {
                if (string.IsNullOrEmpty(dto.UserName))
                {
                    return new ValidationResult("UserName is required", new[] { nameof(AddUserDto.UserName) });
                }

                if (string.IsNullOrEmpty(dto.Email) || !new EmailAddressAttribute().IsValid(dto.Email))
                {
                    return new ValidationResult("Invalid Email Address", new[] { nameof(AddUserDto.Email) });
                }

                if (string.IsNullOrEmpty(dto.PasswordHash) || dto.PasswordHash.Length < 8)
                {
                    return new ValidationResult("Password must be at least 8 characters long", new[] { nameof(AddUserDto.PasswordHash) });
                }

                if (!string.IsNullOrEmpty(dto.PhoneNumber) && !new PhoneAttribute().IsValid(dto.PhoneNumber))
                {
                    return new ValidationResult("Invalid Phone Number", new[] { nameof(AddUserDto.PhoneNumber) });
                }

                /*if (dto.Age < 18 || dto.Age > 100)
                {
                    return new ValidationResult("Age must be between 18 and 100", new[] { nameof(AddUserDto.Age) });
                }

                if (!Enum.IsDefined(typeof(Gender), dto.Gender))
                {
                    return new ValidationResult("Invalid Gender", new[] { nameof(AddUserDto.Gender) });
                }*/

                return ValidationResult.Success;
            }
        }
    }
} 