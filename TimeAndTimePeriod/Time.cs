namespace TimeAndTimePeriod;

public struct Time : IEquatable<Time>, IComparable<Time>
{
    /// <summary>
    /// Gets the hours for the time object.
    /// </summary>
    public byte Hours { get; }

    /// <summary>
    /// Gets the minutes for the time object.
    /// </summary>
    public byte Minutes { get; }

    /// <summary>
    /// Gets the seconds for the time object.
    /// </summary>
    public byte Seconds { get; }

    /// <summary>
    /// Gets the milliseconds for the time object.
    /// </summary>
    public int Milliseconds { get; }

    /// <summary>
    /// Initializes a new instance of the Time class with the specified time components.
    /// </summary>
    /// <param name="hours">The value of the hours component (0-23).</param>
    /// <param name="minutes">The value of the minutes component (0-59). Default is 0.</param>
    /// <param name="seconds">The value of the seconds component (0-59). Default is 0.</param>
    /// <param name="milliseconds">The value of the milliseconds component (0-999). Default is 0.</param>
    /// <exception cref="ArgumentException">Thrown when any of the time components exceeds their valid range.</exception>
    public Time(byte hours, byte minutes = 0, byte seconds = 0, int milliseconds = 0)
    {
        if (hours >= 24 || minutes >= 60 || seconds >= 60 || milliseconds >= 1000)
            throw new ArgumentException("Invalid time.");

        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        Milliseconds = milliseconds;
    }

    /// <summary>
    /// Initializes a new instance of the Time class using the specified string representation of time.
    /// The string format should be in the format "HH:MM:SS".
    /// </summary>
    /// <param name="string">The string representation of time.</param>
    /// <exception cref="ArgumentException">Thrown when the string format is invalid.</exception>
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

    /// <summary>
    /// Converts the time object to its equivalent string representation.
    /// </summary>
    /// <returns>A string representation of the time object.</returns>
    public override string ToString()
    {
        if (Milliseconds == 0)
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }
        else
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}:{Milliseconds:D3}";
        }
    }

    #region IEquatable<Time>
    /// <summary>
    /// Determines whether the current Time object is equal to another Time object.
    /// </summary>
    /// <param name="other">The Time object to compare with the current object.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public bool Equals(Time other)
    {
        return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds && Milliseconds == other.Milliseconds;
    }

    /// <summary>
    /// Determines whether the current Time object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Time timeOtherTime)
            return Equals(timeOtherTime);

        return false;
    }

    /// <summary>
    /// Gets the hash code for the Time object.
    /// </summary>
    /// <returns>The hash code for the Time object.</returns>
    public override int GetHashCode()
    {
        return Hours.GetHashCode() ^ Minutes.GetHashCode() ^ Seconds.GetHashCode() ^ Milliseconds.GetHashCode();
    }

    /// <summary>
    /// Determines whether two Time objects are equal.
    /// </summary>
    /// <param name="time1">The first Time object to compare.</param>
    /// <param name="time2">The second Time object to compare.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public static bool operator ==(Time time1, Time time2)
    {
        return time1.Equals(time2);
    }

    /// <summary>
    /// Determines whether two Time objects are not equal.
    /// </summary>
    /// <param name="time1">The first Time object to compare.</param>
    /// <param name="time2">The second Time object to compare.</param>
    /// <returns>True if the objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Time time1, Time time2)
    {
        return !time1.Equals(time2);
    }
    #endregion

    #region IComparable<Time>
    /// <summary>
    /// Compares the current Time object with another Time object.
    /// </summary>
    /// <param name="other">The Time object to compare with the current object.</param>
    /// <returns>
    /// A value indicating the relative order of the objects being compared. 
    /// Returns a negative integer if the current object is less than the other object, 
    /// zero if they are equal, or a positive integer if the current object is greater than the other object.
    /// </returns>
    public int CompareTo(Time other)
    {
        if (Hours != other.Hours)
            return Hours.CompareTo(other.Hours);
        if (Minutes != other.Minutes)
            return Minutes.CompareTo(other.Minutes);
        if (Seconds != other.Seconds)
            return Minutes.CompareTo(other.Seconds);

        return Milliseconds.CompareTo(other.Milliseconds);
    }

    /// <summary>
    /// Determines whether one Time object is less than another Time object.
    /// </summary>
    /// <param name="time1">The first Time object to compare.</param>
    /// <param name="time2">The second Time object to compare.</param>
    /// <returns>True if the first object is less than the second object; otherwise, false.</returns>
    public static bool operator <(Time time1, Time time2)
    {
        return time1.CompareTo(time2) < 0;
    }

    /// <summary>
    /// Determines whether one Time object is less than or equal to another Time object.
    /// </summary>
    /// <param name="time1">The first Time object to compare.</param>
    /// <param name="time2">The second Time object to compare.</param>
    /// <returns>True if the first object is less than or equal to the second object; otherwise, false.</returns>
    public static bool operator <=(Time time1, Time time2)
    {
        return time1.CompareTo(time2) <= 0;
    }

    /// <summary>
    /// Determines whether one Time object is greater than another Time object.
    /// </summary>
    /// <param name="time1">The first Time object to compare.</param>
    /// <param name="time2">The second Time object to compare.</param>
    /// <returns>True if the first object is greater than the second object; otherwise, false.</returns>
    public static bool operator >(Time time1, Time time2)
    {
        return time1.CompareTo(time2) > 0;
    }

    /// <summary>
    /// Determines whether one Time object is greater than or equal to another Time object.
    /// </summary>
    /// <param name="time1">The first Time object to compare.</param>
    /// <param name="time2">The second Time object to compare.</param>
    /// <returns>True if the first object is greater than or equal to the second object; otherwise, false.</returns>
    public static bool operator >=(Time time1, Time time2)
    {
        return time1.CompareTo(time2) >= 0;
    }
    #endregion

    #region Adding
    private Time Adding(TimePeriod period)
    {
        var timeTotalSeconds = Hours * 3600L + Minutes * 60L + Seconds;
        var timeTotalMilliseconds = timeTotalSeconds * 1000L + Milliseconds;
        var timeNewTotalMilliseconds = timeTotalMilliseconds + period.TotalSeconds * 1000L + period.Milliseconds;

        var timeNewTotalSeconds = (long)(timeNewTotalMilliseconds / 1000L) % 86400L;
        if (timeNewTotalSeconds < 0)
            timeNewTotalSeconds += 86400L;

        var timeNewHours = (byte)(timeNewTotalSeconds / 3600L);
        var timeNewMinutes = (byte)((timeNewTotalSeconds / 60L) % 60L);
        var timeNewSeconds = (byte)(timeNewTotalSeconds % 60L);
        var timeNewMilliseconds = (int)(timeNewTotalMilliseconds % 1000L);

        return new Time(timeNewHours, timeNewMinutes, timeNewSeconds, timeNewMilliseconds);
    }

    /// <summary>
    /// Adds a TimePeriod to a Time object and returns a new Time object representing the result.
    /// </summary>
    /// <param name="time">The Time object to add.</param>
    /// <param name="period">The TimePeriod to add.</param>
    /// <returns>A new Time object representing the result of the addition.</returns>
    public static Time Plus(Time time, TimePeriod period)
    {
        return time.Adding(period);
    }

    /// <summary>
    /// Adds a TimePeriod to a Time object and returns a new Time object representing the result.
    /// </summary>
    /// <param name="time">The Time object to add.</param>
    /// <param name="period">The TimePeriod to add.</param>
    /// <returns>A new Time object representing the result of the addition.</returns>
    public static Time operator +(Time time, TimePeriod period)
    {
        return Plus(time, period);
    }
    #endregion

    #region Substraction
    private Time Substracting(TimePeriod period)
    {
        var timeTotalSeconds = Hours * 3600L + Minutes * 60L + Seconds;
        var timeTotalMilliseconds = timeTotalSeconds * 1000L + Milliseconds;
        var timeNewTotalMilliseconds = timeTotalMilliseconds - period.TotalSeconds * 1000L - period.Milliseconds;

        if (timeNewTotalMilliseconds < 0)
            timeNewTotalMilliseconds += 86400L * 1000L;

        var timeNewTotalSeconds = (long)(timeNewTotalMilliseconds / 1000L) % 86400L;

        var timeNewHours = (byte)(timeNewTotalSeconds / 3600L);
        var timeNewMinutes = (byte)((timeNewTotalSeconds / 60L) % 60L);
        var timeNewSeconds = (byte)(timeNewTotalSeconds % 60L);
        var timeNewMilliseconds = (int)(timeNewTotalMilliseconds % 1000L);

        return new Time(timeNewHours, timeNewMinutes, timeNewSeconds, timeNewMilliseconds);
    }

    /// <summary>
    /// Subtracts a TimePeriod from a Time object and returns a new Time object representing the result.
    /// </summary>
    /// <param name="time">The Time object to subtract from.</param>
    /// <param name="period">The TimePeriod to subtract.</param>
    /// <returns>A new Time object representing the result of the subtraction.</returns>
    public static Time Minus(Time time, TimePeriod period)
    {
        return time.Substracting(period);
    }

    /// <summary>
    /// Subtracts a TimePeriod from a Time object and returns a new Time object representing the result.
    /// </summary>
    /// <param name="t1">The Time object to subtract from.</param>
    /// <param name="period">The TimePeriod to subtract.</param>
    /// <returns>A new Time object representing the result of the subtraction.</returns>
    public static Time operator -(Time t1, TimePeriod period)
    {
        return Minus(t1, period);
    }
    #endregion
}