namespace TimeAndTimePeriod;

public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
{
    public long TotalSeconds { get; }

    public TimePeriod(long totalSeconds)
    {
        if (totalSeconds < 0) throw new ArgumentException("Invalid time period");

        TotalSeconds = totalSeconds;
    }

    public TimePeriod(int hours, byte minutes = 0, byte seconds = 0)
    {
        if (minutes >= 60 || seconds >= 60)
            throw new ArgumentException("Invalid time.");
        
        long totalSeconds = hours * 3600L + minutes * 60L + seconds;
        
        if(totalSeconds < 0) throw new ArgumentException("Invalid time period");

        TotalSeconds = totalSeconds;
    }
    
    public TimePeriod(string timeString)
    {
        var parts = timeString.Split(':');
        if (parts.Length != 3)
            throw new ArgumentException("Invalid time format.");

        if (!int.TryParse(parts[0], out var hours) || !byte.TryParse(parts[1], out var minutes) ||
            !byte.TryParse(parts[2], out var seconds))
        {
            throw new ArgumentException("Invalid time format.");
        }

        long totalSeconds = hours * 3600L + minutes * 60L + seconds;
        if (totalSeconds < 0)
            throw new ArgumentException("Invalid time period.");

        TotalSeconds = totalSeconds;
    }
    
    public override string ToString()
    {
        long hours = TotalSeconds / 3600;
        long minutes = (TotalSeconds / 60) % 60;
        long seconds = TotalSeconds % 60;
        
        return $"{hours}:{minutes:D2}:{seconds:D2}";
    }
    
    #region IEquatable<TimePeriod>
    public bool Equals(TimePeriod other)
    {
        return TotalSeconds == other.TotalSeconds;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is TimePeriod otherPeriod)
            return Equals(otherPeriod);

        return false;
    }

    public override int GetHashCode()
    {
        return TotalSeconds.GetHashCode();
    }
    
    public static bool operator ==(TimePeriod period1, TimePeriod period2)
    {
        return period1.Equals(period2);
    }

    public static bool operator !=(TimePeriod period1, TimePeriod period2)
    {
        return !period1.Equals(period2);
    }
    #endregion
    
    #region IComparable<TimePeriod>
    public int CompareTo(TimePeriod other)
    {
        return TotalSeconds.CompareTo(other.TotalSeconds);
    }
    
    public static bool operator <(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) < 0;
    }

    public static bool operator <=(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) <= 0;
    }

    public static bool operator >(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) > 0;
    }

    public static bool operator >=(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) >= 0;
    }
    #endregion
    
    public TimePeriod Plus(TimePeriod other)
    {
        return new TimePeriod(TotalSeconds + other.TotalSeconds);
    }
    public static TimePeriod operator +(TimePeriod period1, TimePeriod period2)
    {
        return new TimePeriod(period1.TotalSeconds + period2.TotalSeconds);
    }

    public static TimePeriod operator -(TimePeriod period1, TimePeriod period2)
    {
        return new TimePeriod(period1.TotalSeconds - period2.TotalSeconds);
    }
}