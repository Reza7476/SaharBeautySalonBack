namespace BeautySalon.Common.Interfaces;
public interface IPagination
{
    int Offset { get; init; }
    int limit { get; init; }
}
