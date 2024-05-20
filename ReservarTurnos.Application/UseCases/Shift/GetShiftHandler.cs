using MediatR;
using ReservarTurnos.Application.Dtos;
using ReservarTurnos.Application.Mappers;
using ReservarTurnos.Infraestructure;

namespace ReservarTurnos.Application.UseCases.Shift;

public class GetShiftHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetShiftQuery, List<ShiftDto>>
{
    public Task<List<ShiftDto>> Handle(GetShiftQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<List<ShiftDto>>([.. unitOfWork.ShiftRepository.GetShifts(request.ServiceId, request.StartDate, request.EndDate).ToQueryableDto()]);
    }
}
