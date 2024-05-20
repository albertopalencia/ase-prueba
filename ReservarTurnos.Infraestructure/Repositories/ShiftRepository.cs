using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservarTurnos.Domain;
using ReservarTurnos.Infraestructure.Common;
using ReservarTurnos.Infraestructure.Interfaces;
using ReservarTurnos.Infraestructure.Persistence; 
namespace ReservarTurnos.Infraestructure.Repositories;

public class ShiftRepository(ApplicationDbContext context) : BaseRepository<Shift>(context), IShiftRepository
{
    public List<Shift> GetShifts(int serviceId, DateTime startDate, DateTime endDate)
    {
        return context.Shifts.FromSqlRaw("EXEC dbo.GenerarTurnos @ServicioId, @FechaInicio, @FechaFin",
                                   new SqlParameter("@ServicioId", serviceId),
                                   new SqlParameter("@FechaInicio", startDate),
                                   new SqlParameter("@FechaFin", endDate)).AsEnumerable().ToList();
    }
}
