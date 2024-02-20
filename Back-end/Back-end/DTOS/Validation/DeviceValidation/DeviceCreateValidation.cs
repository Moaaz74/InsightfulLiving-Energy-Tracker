using Back_end.DTOS.Device;
using Back_end.DTOS.Home;
using Back_end.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Back_end.DTOS.Validation.DeviceValidation
{
    public class DeviceCreateValidation : AbstractValidator<DeviceCreateDto>
    {
        

        public DeviceCreateValidation()
        {
            
            RuleFor(D => D.EnergyType)
                  .NotEmpty().WithMessage("{PropertyName} is must not empty.")
                  .NotNull().WithMessage("{PropertyName} is reqiured").MaximumLength(50).WithMessage("{PropertyName} Must Be With MaximumLength 50 "); 

            RuleFor(D=> D.RoomId)
           .NotEmpty().WithMessage("{PropertyName} is must not empty.")
           .NotNull().WithMessage("{PropertyName} is reqiured")
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
