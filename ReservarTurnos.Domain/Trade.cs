using ReservarTurnos.Domain.Common;

namespace ReservarTurnos.Domain
{
    public class Trade : Entity
    { 
        public string Name { get; set; }
        public int MaximumCapacity { get; set; }
        public ICollection<Service>? Services { get; set; }
    }
}
