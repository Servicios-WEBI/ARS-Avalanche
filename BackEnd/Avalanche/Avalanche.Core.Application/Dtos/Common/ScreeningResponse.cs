namespace Avalanche.Core.Application.Dtos.Common
{
    public class ScreeningResponse
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartHour { get; set; }
        public TimeOnly EndHour { get; set; }
        public string MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string TariffName { get; set; }
        public double Price { get; set; }
    }
}
