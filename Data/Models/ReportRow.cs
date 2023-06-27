namespace Data.Models;

public class ReportRow
{
    public DateTime Date { get; set; }
    public string Name { get; set; } = null!;
    public string Action { get; set; } = null!;
    public int DayOfWeek { get; set; }
}