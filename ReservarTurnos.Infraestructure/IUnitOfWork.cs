using ReservarTurnos.Infraestructure.Interfaces;

namespace ReservarTurnos.Infraestructure;

public interface IUnitOfWork : IDisposable
{
    public IServiceRepository ServiceRepository { get; }
    public IShiftRepository ShiftRepository { get; }
    public ITradeRepository TradeRepository { get; }

    Task<int> SaveAsync(CancellationToken cancellationToken = default);

    int Save();
}
