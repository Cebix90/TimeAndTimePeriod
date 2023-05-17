using System.Runtime.CompilerServices;

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
        
        long timeTotalSeconds = hours * 3600L + minutes * 60L + seconds;
        
        if(timeTotalSeconds < 0) throw new ArgumentException("Invalid time period");

        TotalSeconds = timeTotalSeconds;
    }
    
    public TimePeriod(string @string)
    {
        var timeParts = @string.Split(':');
        if (timeParts.Length != 3)
            throw new ArgumentException("Invalid time format.");

        if (!int.TryParse(timeParts[0], out var timeHours) || !byte.TryParse(timeParts[1], out var timeMinutes) ||
            !byte.TryParse(timeParts[2], out var timeSeconds))
        {
            throw new ArgumentException("Invalid time format.");
        }

        long timeTotalSeconds = timeHours * 3600L + timeMinutes * 60L + timeSeconds;
        if (timeTotalSeconds < 0)
            throw new ArgumentException("Invalid time period.");

        TotalSeconds = timeTotalSeconds;
    }
    
    public override string ToString()
    {
        long timeHours = TotalSeconds / 3600;
        long timeMinutes = (TotalSeconds / 60) % 60;
        long timeSeconds = TotalSeconds % 60;
        
        return $"{timeHours}:{timeMinutes:D2}:{timeSeconds:D2}";
    }
    
    #region IEquatable<TimePeriod>
    public bool Equals(TimePeriod other)
    {
        return TotalSeconds == other.TotalSeconds;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is TimePeriod timeOtherPeriod)
            return Equals(timeOtherPeriod);

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
    
    #region Adding
    private TimePeriod Adding(TimePeriod period1)
    {
        return new TimePeriod(TotalSeconds + period1.TotalSeconds);
    }
    
    public static TimePeriod Plus(TimePeriod period1, TimePeriod period2)
    {
        return period1.Adding(period2);
    }
    public static TimePeriod operator +(TimePeriod period1, TimePeriod period2)
    {
        return Plus(period1, period2);
    }
    #endregion

    #region Substracting
    private TimePeriod Substracting(TimePeriod period1)
    {
        var substractedTotalSecond = TotalSeconds - period1.TotalSeconds;
        return substractedTotalSecond > 0 ? new TimePeriod(substractedTotalSecond) : new TimePeriod(0);
    }
    public static TimePeriod Minus(TimePeriod period1, TimePeriod period2)
    {
        return period1.Substracting(period2);
    }
    public static TimePeriod operator -(TimePeriod period1, TimePeriod period2)
    {
        return Minus(period1, period2);
    }
    #endregion
}