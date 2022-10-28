using API.Dto;
using FluentValidation;
using Newtonsoft.Json;
using Utils.Enum;

namespace API.Users;

public class UserBodyMetricValidator : AbstractValidator<UserBodyMetricDto>
{
    public UserBodyMetricValidator()
    {
        RuleFor(e => e.Height).InclusiveBetween(150, 200)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"User's height is not within the allowed range (User's height: {e.Height} - Allowed height range: 150 - 200 [cm])."));
        RuleFor(e => e.Weight).InclusiveBetween(50.0, 200.0)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"User's weight is not within the allowed range (User's weight: {Math.Round(e.Weight, 2)} - Allowed weight range: 50 - 200 [kg])."));
        RuleFor(e => e.BodyMassIndex).InclusiveBetween(16.00, 34.99)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"User's BMI is not within the allowed range (User's BMI: {Math.Round(e.BodyMassIndex, 2)} - Allowed BMI range: 16.00 - 34.99)."));
        RuleFor(e => e.PhysicalActivity)
            .Must(e => PhysicalActivity.FromReadableName(e) != null)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e}” does not correspond to a valid physical activity value.
Recognized values are:
{string.Join('\n', PhysicalActivity.List)}"));
    }
}