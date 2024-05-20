using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservarTurnos.Application.Dtos;
using ReservarTurnos.Application.UseCases.Shift;

namespace ReservarTurnos.Presentation.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShiftController(ISender sender, IValidator<ShiftRequestDto> validator) : ControllerBase
    {
        [HttpPost("Generar")]
        public async Task<IActionResult> GenerateShifts([FromBody] ShiftRequestDto request, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult result = validator.Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            GetShiftQuery query = new(request.ServiceId, DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate));
            var response = await sender.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}
