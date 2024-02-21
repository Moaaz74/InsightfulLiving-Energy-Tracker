using Back_end.DTOS.Home;
using Back_end.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Back_end.DTOS.Validation.HomeValidation
{
    public class HomeCreateValidation:AbstractValidator<HomeCreateDto>
    {
       
        public HomeCreateValidation()
        {
            // Rule for UserId with improved message
            RuleFor(h => h.UserId)
                .Custom((userId, context) =>
                {
                    if (userId == null || userId == "null")
                    {
                        context.AddFailure("UserId", "UserId cannot be null.");
                    }
                    else if (string.IsNullOrWhiteSpace(userId.ToString()))
                    {
                        context.AddFailure("UserId", "UserId cannot be empty or consist only of whitespace.");
                    }
                });

            // Rule for NumberOfRooms with improved clarity
            RuleFor(h => h.NumberOfRooms)
                .Custom((numberOfRooms, context) =>
                {
                    if (numberOfRooms == null)
                    {
                        context.AddFailure("NumberOfRooms", "NumberOfRooms cannot be null.");
                    }
                    else if (!int.TryParse(numberOfRooms.ToString(), out _))
                    {
                        context.AddFailure("NumberOfRooms", "NumberOfRooms must be a valid integer.");
                    }
                });
        }

        public List<string> ListError(FluentValidation.Results.ValidationResult validationResult)
        {
            // Get the list of validation failures
            var validationFailures = validationResult.Errors;

            // Create a list to hold the error messages
            var errorMessages = new List<string>();

            // Iterate over the validation failures
            foreach (var failure in validationFailures)
            {
                // Add the error message to the list
                errorMessages.Add(failure.ErrorMessage);
            }

            return errorMessages;
        }




    }
}
