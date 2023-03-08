namespace Domain.ValueObjects;

public class BirthDay
{
    private readonly DateOnly _date;

    public BirthDay(DateTime dateTime)
    {
        _date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public bool IsToday(TimeZoneInfo? info = null)
    {
        var timeZoneInfo = info ?? TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time(Mexico)");
        var today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
        return this.IsEquals(today);
    }

    public bool IsEquals(DateTime dateTime)
        => _date == new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

    public bool IsEquals(BirthDay value)
        => _date == value?._date;

    public override bool Equals(object? obj)
    {
        if (obj is DateTime dt)
        {
            return this.IsEquals(dt);
        }

        if (obj is BirthDay bd)
        {
            return this.IsEquals(bd);
        }

        return false;
    }

    public static bool operator ==(BirthDay a, BirthDay b)
        => a.IsEquals(b);

    public static bool operator !=(BirthDay a, BirthDay b)
        => !a.IsEquals(b);
}