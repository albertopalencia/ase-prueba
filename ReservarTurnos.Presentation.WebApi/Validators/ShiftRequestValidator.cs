using FluentValidation;
using ReservarTurnos.Application.Dtos;
using System.Globalization;

namespace ReservarTurnos.Application.Validators
{

    public class ShiftRequestValidator : AbstractValidator<ShiftRequestDto>
    {
        public ShiftRequestValidator()
        {
            RuleFor(x => x.ServiceId)
                .GreaterThan(0)
                .WithMessage("ServiceId debe ser mayor a 0.");

            RuleFor(x => x.StartDate)                
                .NotEmpty()
                .WithMessage("Fecha Inicio no debe estar vacío.")
                .Must(BeValidDate)
                .WithMessage("Fecha Inicio debe estar en el formato dd/MM/yyyy.")
                .LessThan(x => x.EndDate)
                .WithMessage("Fecha Inicio debe ser menor que Fecha Fin.");
                

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("Fecha Fin no debe estar vacío.")
                .Must(BeValidDate)
                .WithMessage("Fecha Fin debe estar en el formato dd/MM/yyyy.")
                .GreaterThan(x => x.StartDate)
                .WithMessage("Fecha Fin debe ser mayor que Fecha Inicio.");
        }

        private bool BeValidDate(string date)
        {
            return DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }

}
