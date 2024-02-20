using Back_end.DTOS.Home;
using Back_end.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Back_end.DTOS.Validation.HomeValidation
{
    public class HomeUpdateValidation : AbstractValidator<HomeUpdateDto>
    {
        public readonly UserManager<ApplicationUser> _manager;

        public HomeUpdateValidation(UserManager<ApplicationUser> manager)
        {
            _manager = manager;

            RuleFor(h => h.UserId)
                  .NotEmpty().WithMessage("{UserId} is required.")
                  .NotNull();

            RuleFor(h => h.NumberOfRooms)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .Must(BeAnInteger).WithMessage("{PropertyName} must be an integer.");

            RuleFor(p => p.UserId).MustAsync(Cheack).WithMessage("User Id Is Not Exixt");
        }

        private bool BeAnInteger(string arg)
        {
            if (arg == null)
                return true;

            return int.TryParse(arg.ToString(), out _);
        }

        private async Task<bool> Cheack(string arg1, CancellationToken token)
        {
            var result = await _manager.FindByIdAsync(arg1);
            if (result == null)
            {
                return false;
            }
            return true;
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

