namespace TimeAndTimePeriod;

public struct Time : IEquatable<Time>, IComparable<Time>
{
    public int Hours { get; }
    public byte Minutes { get; }
    public byte Seconds { get; }

    public Time(int hours, byte minutes = 0, byte seconds = 0)
    {
        if (hours >= 24 || minutes >= 60 || seconds >= 60)
            throw new ArgumentException("Invalid time.");
        
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }
    
    public Time(string timeString)
    {
        var parts = timeString.Split(':');
        if (parts.Length != 3)
            throw new ArgumentException("Invalid time format.");

        if (!int.TryParse(parts[0], out var hours) || !byte.TryParse(parts[1], out var minutes) ||
            !byte.TryParse(parts[2], out var seconds))
        {
            throw new ArgumentException("Invalid time format.");
        }

        this = new Time(hours, minutes, seconds);
    }

    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
    }
    
    #region IEquatable<Time>
    public bool Equals(Time other)
    {
        return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is Time otherTime)
            return Equals(otherTime);

        return false;
    }

    public override int GetHashCode()
    {
        return Hours.GetHashCode() ^ Minutes.GetHashCode() ^ Seconds.GetHashCode();
    }
    
    public static bool operator ==(Time time1, Time time2)
    {
        return time1.Equals(time2);
    }

    public static bool operator !=(Time time1, Time time2)
    {
        return !time1.Equals(time2);
    }
    #endregion
    
    #region IComparable<Time>
    public int CompareTo(Time other)
    {
        if (Hours != other.Hours)
            return Hours.CompareTo(other.Hours);
        if (Minutes != other.Minutes)
            return Minutes.CompareTo(other.Minutes);
        
        return Seconds.CompareTo(other.Seconds);
    }
    
    public static bool operator <(Time time1, Time time2)
    {
        return time1.CompareTo(time2) < 0;
    }

    public static bool operator <=(Time time1, Time time2)
    {
        return time1.CompareTo(time2) <= 0;
    }

    public static bool operator >(Time time1, Time time2)
    {
        return time1.CompareTo(time2) > 0;
    }

    public static bool operator >=(Time time1, Time time2)
    {
        return time1.CompareTo(time2) >= 0;
    }
    #endregion
    
    public static Time operator +(Time time, TimePeriod period)
    {
        var totalSeconds = time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
        var newTotalSeconds = (totalSeconds + period.TotalSeconds) % 86400;
        if (newTotalSeconds < 0)
            newTotalSeconds += 86400;

        var newHours = (byte)(newTotalSeconds / 3600);
        var newMinutes = (byte)((newTotalSeconds / 60) % 60);
        var newSeconds = (byte)(newTotalSeconds % 60);

        return new Time(newHours, newMinutes, newSeconds);
    }
    
    /*public Time Plus(TimePeriod period)
    {
        return Plus(this, period);
    }*/
}