using Back_end.DTOS.Device;
using FluentValidation;

namespace Back_end.DTOS.Validation.DeviceValidation
{
    public class DeviceUpdateValidation : AbstractValidator<DeviceUpdateDto>
    {


        public DeviceUpdateValidation()
        {
            // Rule for EnergyType with proper checks and message
            RuleFor(d => d.EnergyType)
                .Custom((energyType, context) =>
                {
                    if (energyType == null || energyType == "null")
                    {
                        context.AddFailure("EnergyType", "EnergyType cannot be null.");
                    }
                    else if (string.IsNullOrWhiteSpace(energyType))
                    {
                        context.AddFailure("EnergyType", "EnergyType cannot be empty or consist only of whitespace.");
                    }
                    else if (energyType.Length > 50)
                    {
                        context.AddFailure("EnergyType", "EnergyType cannot exceed 50 characters.");
                    }
                });

            // Rule for RoomId with proper checks and message
            RuleFor(d => d.RoomId)
                .Custom((roomId, context) =>
                {
                    if (roomId == null || roomId == "null")
                    {
                        context.AddFailure("RoomId", "RoomId cannot be null.");
                    }
                    else if (!int.TryParse(roomId.ToString(), out _))
                    {
                        context.AddFailure("RoomId", "RoomId must be a valid integer.");
                    }
                });
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

