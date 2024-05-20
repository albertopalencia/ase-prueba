using ReservarTurnos.Application.Dtos;
using MediatR;

namespace ReservarTurnos.Application.UseCases.Shift;

public record GetShiftQuery(int ServiceId, DateTime StartDate, DateTime EndDate) : IRequest<List<ShiftDto>>;
