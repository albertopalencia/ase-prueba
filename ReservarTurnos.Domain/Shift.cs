using ReservarTurnos.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservarTurnos.Domain
{
    public class Shift : Entity
    { 
        public DateTime ShiftDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool State { get; set; }
        public int ServiceId { get; set; }
    }
}
