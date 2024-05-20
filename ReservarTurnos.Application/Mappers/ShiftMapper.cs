using ReservarTurnos.Application.Dtos;
using ReservarTurnos.Domain;

namespace ReservarTurnos.Application.Mappers;

public static class ShiftMapper
{
    public static List<ShiftDto> ToQueryableDto(this List<Shift> query) =>
        query.Select(x => new ShiftDto(x.ShiftDate, x.StartTime, x.EndTime)).ToList();

    public static ShiftDto ToEntity(this Shift shift) =>
        new(shift.ShiftDate, shift.StartTime, shift.EndTime); 
}
