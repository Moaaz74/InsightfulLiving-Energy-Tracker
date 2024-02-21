using Back_end.DTOS.Room;
using Back_end.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Back_end.DTOS.Validation.RoomValidation
{
    public class RoomCreateValidation:AbstractValidator<RoomCreateDto>
    {

        public RoomCreateValidation()
        {
            // Rule for HomeId with proper null check and message
            RuleFor(r => r.HomeId)
                .Custom((homeId, context) =>
                {
                    if (homeId == null || homeId == "null")
                    {
                        context.AddFailure("HomeId", "HomeId cannot be null.");
                    }
                    else if (!int.TryParse(homeId.ToString(), out _))
                    {
                        context.AddFailure("HomeId", "HomeId must be a valid integer.");
                    }
                });

            // Rule for NumberOfDevices with proper null check and message
            RuleFor(r => r.NumberOfDevices)
                .Custom((numberOfDevices, context) =>
                {
                    if (numberOfDevices == null || numberOfDevices == "null")
                    {
                        context.AddFailure("NumberOfDevices", "NumberOfDevices cannot be null.");
                    }
                    else if (!int.TryParse(numberOfDevices.ToString(), out _))
                    {
                        context.AddFailure("NumberOfDevices", "NumberOfDevices must be a valid integer.");
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
