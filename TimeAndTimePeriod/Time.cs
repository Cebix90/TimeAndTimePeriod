namespace TimeAndTimePeriod;

public struct Time : IEquatable<Time>, IComparable<Time>
{
    public byte Hours { get; }
    public byte Minutes { get; }
    public byte Seconds { get; }

    public Time(byte hours, byte minutes = 0, byte seconds = 0)
    {
        if (hours >= 24 || minutes >= 60 || seconds >= 60)
            throw new ArgumentException("Invalid time.");
        
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }
    
    public Time(string @string)
    {
        var timeParts = @string.Split(':');
        if (timeParts.Length != 3)
            throw new ArgumentException("Invalid time format.");

        if (!byte.TryParse(timeParts[0], out var timeHours) || !byte.TryParse(timeParts[1], out var timeMinutes) ||
            !byte.TryParse(timeParts[2], out var timeSeconds))
        {
            throw new ArgumentException("Invalid time format.");
        }

        this = new Time(timeHours, timeMinutes, timeSeconds);
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
        if (obj is Time timeOtherTime)
            return Equals(timeOtherTime);

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
    
    #region Adding
    private Time Adding(TimePeriod period)
    {
        var timeTotalSeconds = Hours * 3600 + Minutes * 60 + Seconds;
        var timeNewTotalSeconds = (timeTotalSeconds + period.TotalSeconds) % 86400;
        if (timeNewTotalSeconds < 0)
            timeNewTotalSeconds += 86400;

        var timeNewHours = (byte)(timeNewTotalSeconds / 3600);
        var timeNewMinutes = (byte)((timeNewTotalSeconds / 60) % 60);
        var timeNewSeconds = (byte)(timeNewTotalSeconds % 60);

        return new Time(timeNewHours, timeNewMinutes, timeNewSeconds);
    }
    
    public static Time Plus(Time time, TimePeriod period)
    {
        return time.Adding(period);
    }
    public static Time operator +(Time time, TimePeriod period)
    {
        return Plus(time, period);
    }
    #endregion
    
    #region Substraction
    public Time Minus(TimePeriod period)
    {
        var timeTotalSeconds = Seconds + (int)Minutes * 60 + (int)Hours * 3600;
        timeTotalSeconds -= (int)period.TotalSeconds;

        if (timeTotalSeconds < 0)
        {
            timeTotalSeconds += 86400; 
        }

        var timeHours = timeTotalSeconds / 3600;
        var timeMinutes = (timeTotalSeconds % 3600) / 60;
        var timeSeconds = (timeTotalSeconds % 3600) % 60;
        
        return new Time((byte)timeHours, (byte)timeMinutes, (byte)timeSeconds);
    }

    public static Time Minus(Time time, TimePeriod period)
    {
        return time.Minus(period);
    }

    public static Time operator -(Time t1, TimePeriod period)
    {
        return Minus(t1, period);
    }
    #endregion
}