using ReservarTurnos.Domain.Common;

namespace ReservarTurnos.Domain
{
    public class Service : Entity
    {        
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public int TradeId { get; set; }
        public Trade Trade { get; set; }
        public ICollection<Shift> Shifts { get; set; }
    }
}
