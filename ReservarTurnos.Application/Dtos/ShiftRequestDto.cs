namespace ReservarTurnos.Application.Dtos;

public class ShiftRequestDto
{
    public int ServiceId { get; set; }
    public string StartDate { get; set; }

    public string EndDate { get; set; }

}
