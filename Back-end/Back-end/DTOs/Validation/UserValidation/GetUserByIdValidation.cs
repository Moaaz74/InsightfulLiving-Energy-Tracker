using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOs
{
    public class GetUserByIdValidation
    {
        public static class Validator
        {
            public static ValidationResult Validate(GetUserByIdDto dto)
            {
                if (string.IsNullOrEmpty(dto.UserId))
                {
                    return new ValidationResult("UserId is required", new[] { nameof(GetUserByIdDto.UserId) });
                }

                return ValidationResult.Success; 
            }
        }
    }
}
