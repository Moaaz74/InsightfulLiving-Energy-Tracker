using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOs
{
    public static class UpdateUserValidation
    {
        public static class Validator
        {
            public static ValidationResult Validate(UpdateUserDto dto)
            {
                if (string.IsNullOrEmpty(dto.UserName))
                {
                    return new ValidationResult("UserId is required", new[] { nameof(UpdateUserDto.UserName) });
                }

                if (string.IsNullOrEmpty(dto.Email) || !new EmailAddressAttribute().IsValid(dto.Email))
                {
                    return new ValidationResult("Invalid Email Address", new[] { nameof(AddUserDto.Email) });
                }


                if (!string.IsNullOrEmpty(dto.PhoneNumber) && !new PhoneAttribute().IsValid(dto.PhoneNumber))
                {
                    return new ValidationResult("Invalid Phone Number", new[] { nameof(AddUserDto.PhoneNumber) });
                }

                return ValidationResult.Success;
            }
        }
    }
} 