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
        

            RuleFor(r => r.HomeId)
                  .NotEmpty().WithMessage("{HomeId} is required.")
                  .NotNull().Must(BeAnInteger).WithMessage("{PropertyName} must be an integer."); ;

            RuleFor(r=>r.NumberOfDevices)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .Must(BeAnInteger).WithMessage("{PropertyName} must be an integer.");

           
        }

        private bool BeAnInteger(string arg)
        {
            if (arg == null)
                return true;

            return int.TryParse(arg.ToString(), out _);
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
