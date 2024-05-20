using ReservarTurnos.Infraestructure.Interfaces;
using ReservarTurnos.Infraestructure.Persistence;

namespace ReservarTurnos.Infraestructure;

public class UnitOfWork(ApplicationDbContext context, IServiceRepository serviceRepository, ITradeRepository tradeRepository, IShiftRepository shiftRepository) : IUnitOfWork
{
    public IServiceRepository ServiceRepository => serviceRepository;

    public ITradeRepository TradeRepository => tradeRepository;

    public IShiftRepository ShiftRepository => shiftRepository;

    public void Dispose() => context.Dispose();

    public int Save() => context.SaveChanges();

    public Task<int> SaveAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);

}
