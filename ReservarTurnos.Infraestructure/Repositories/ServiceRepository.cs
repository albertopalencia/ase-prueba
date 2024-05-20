using ReservarTurnos.Domain;
using ReservarTurnos.Infraestructure.Common;
using ReservarTurnos.Infraestructure.Interfaces;
using ReservarTurnos.Infraestructure.Persistence;

namespace ReservarTurnos.Infraestructure.Repositories;

public class ServiceRepository(ApplicationDbContext context) : BaseRepository<Service>(context), IServiceRepository
{
}
