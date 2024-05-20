using ReservarTurnos.Domain;
using ReservarTurnos.Infraestructure.Common;

namespace ReservarTurnos.Infraestructure.Interfaces;

public interface IShiftRepository : IBaseRepository<Shift>
{
    List<Shift> GetShifts(int serviceId, DateTime startDate, DateTime endDate);
}
