namespace TimeAndTimePeriod;

public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
{
    /// <summary>
    /// Gets the total number of seconds in the time period.
    /// </summary>
    public long TotalSeconds { get; }

    /// <summary>
    /// Gets the number of milliseconds in the time period.
    /// </summary>
    public int Milliseconds { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified total seconds and milliseconds.
    /// </summary>
    /// <param name="totalSeconds">The total number of seconds in the time period.</param>
    /// <param name="milliseconds">The number of milliseconds in the time period.</param>
    /// <exception cref="ArgumentException">Thrown when the time period is invalid.</exception>
    public TimePeriod(long totalSeconds, int milliseconds)
    {
        if (totalSeconds < 0 || milliseconds < 0 || milliseconds >= 1000)
            throw new ArgumentException("Invalid time period.");

        TotalSeconds = totalSeconds;
        Milliseconds = milliseconds;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified hours, minutes, seconds, and milliseconds.
    /// </summary>
    /// <param name="hours">The number of hours in the time period.</param>
    /// <param name="minutes">The number of minutes in the time period. Default value is 0.</param>
    /// <param name="seconds">The number of seconds in the time period. Default value is 0.</param>
    /// <param name="milliseconds">The number of milliseconds in the time period. Default value is 0.</param>
    /// <exception cref="ArgumentException">Thrown when the time period is invalid.</exception>
    public TimePeriod(int hours, byte minutes = 0, byte seconds = 0, int milliseconds = 0)
    {
        if (minutes >= 60 || seconds >= 60 || milliseconds >= 1000)
            throw new ArgumentException("Invalid time.");

        long timeTotalSeconds = hours * 3600L + minutes * 60L + seconds;

        if (timeTotalSeconds < 0 || milliseconds < 0)
            throw new ArgumentException("Invalid time period.");

        TotalSeconds = timeTotalSeconds;
        Milliseconds = milliseconds;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified time string in the format "hours:minutes:seconds:milliseconds".
    /// </summary>
    /// <param name="string">The string representation of the time period.</param>
    /// <exception cref="ArgumentException">Thrown when the time period format is invalid.</exception>
    public TimePeriod(string @string)
    {
        var timeParts = @string.Split(':');
        if (timeParts.Length != 4)
            throw new ArgumentException("Invalid time format.");

        if (!int.TryParse(timeParts[0], out var timeHours) || !byte.TryParse(timeParts[1], out var timeMinutes) ||
            !byte.TryParse(timeParts[2], out var timeSeconds) || !int.TryParse(timeParts[3], out var timeMilliseconds))
        {
            throw new ArgumentException("Invalid time format.");
        }

        long timeTotalSeconds = timeHours * 3600L + timeMinutes * 60L + timeSeconds;
        if (timeTotalSeconds < 0)
            throw new ArgumentException("Invalid time period.");

        TotalSeconds = timeTotalSeconds;
        Milliseconds = timeMilliseconds;
    }

    /// <summary>
    /// Converts the time period object to its equivalent string representation.
    /// </summary>
    /// <returns>A string representation of the time period object.</returns>
    public override string ToString()
    {
        long timeHours = TotalSeconds / 3600;
        long timeMinutes = (TotalSeconds / 60) % 60;
        long timeSeconds = TotalSeconds % 60;
        long milliseconds = Milliseconds;

        if (milliseconds == 0)
        {
            return $"{timeHours}:{timeMinutes:D2}:{timeSeconds:D2}";
        }
        else
        {
            return $"{timeHours}:{timeMinutes:D2}:{timeSeconds:D2}:{milliseconds:D3}";
        }
    }

    #region IEquatable<TimePeriod>
    /// <summary>
    /// Determines whether the current <see cref="TimePeriod"/> object is equal to another <see cref="TimePeriod"/> object.
    /// </summary>
    /// <param name="other">The <see cref="TimePeriod"/> object to compare with the current object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other"/> object; otherwise, <c>false</c>.</returns>
    public bool Equals(TimePeriod other)
    {
        return TotalSeconds == other.TotalSeconds && Milliseconds == other.Milliseconds;
    }

    /// <summary>
    /// Determines whether the current <see cref="TimePeriod"/> object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="obj"/> parameter; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is TimePeriod timeOtherPeriod)
            return Equals(timeOtherPeriod);

        return false;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="TimePeriod"/> object.</returns>
    public override int GetHashCode()
    {
        return TotalSeconds.GetHashCode() ^ Milliseconds.GetHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="TimePeriod"/> objects are equal.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to compare.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="period1"/> object is equal to the <paramref name="period2"/> object; otherwise, <c>false</c>.</returns>
    public static bool operator ==(TimePeriod period1, TimePeriod period2)
    {
        return period1.Equals(period2);
    }

    /// <summary>
    /// Determines whether two <see cref="TimePeriod"/> objects are not equal.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to compare.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="period1"/> object is not equal to the <paramref name="period2"/> object; otherwise, <c>false</c>.</returns>
    public static bool operator !=(TimePeriod period1, TimePeriod period2)
    {
        return !period1.Equals(period2);
    }
    #endregion

    #region IComparable<TimePeriod>
    /// <summary>
    /// Compares the current <see cref="TimePeriod"/> object with another <see cref="TimePeriod"/> object and returns an integer that indicates whether the current object is shorter, equal to, or longer than the other object.
    /// </summary>
    /// <param name="other">The <see cref="TimePeriod"/> object to compare with the current object.</param>
    /// <returns>
    /// A positive integer if the current object is longer than the <paramref name="other"/> object.
    /// Zero if the current object is equal to the <paramref name="other"/> object.
    /// A negative integer if the current object is shorter than the <paramref name="other"/> object.
    /// </returns>
    public int CompareTo(TimePeriod other)
    {
        return TotalSeconds.CompareTo(other.TotalSeconds);
    }

    /// <summary>
    /// Determines whether one <see cref="TimePeriod"/> object is shorter than another <see cref="TimePeriod"/> object.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to compare.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="period1"/> object is shorter than the <paramref name="period2"/> object; otherwise, <c>false</c>.</returns>
    public static bool operator <(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) < 0;
    }

    /// <summary>
    /// Determines whether one <see cref="TimePeriod"/> object is shorter than or equal to another <see cref="TimePeriod"/> object.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to compare.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="period1"/> object is shorter than or equal to the <paramref name="period2"/> object; otherwise, <c>false</c>.</returns>
    public static bool operator <=(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) <= 0;
    }

    /// <summary>
    /// Determines whether one <see cref="TimePeriod"/> object is longer than another <see cref="TimePeriod"/> object.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to compare.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="period1"/> object is longer than the <paramref name="period2"/> object; otherwise, <c>false</c>.</returns>
    public static bool operator >(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) > 0;
    }

    /// <summary>
    /// Determines whether one <see cref="TimePeriod"/> object is longer than or equal to another <see cref="TimePeriod"/> object.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to compare.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="period1"/> object is longer than or equal to the <paramref name="period2"/> object; otherwise, <c>false</c>.</returns>
    public static bool operator >=(TimePeriod period1, TimePeriod period2)
    {
        return period1.CompareTo(period2) >= 0;
    }
    #endregion

    #region Adding
    private TimePeriod Adding(TimePeriod period1)
    {
        long totalSeconds = TotalSeconds + period1.TotalSeconds;
        int milliseconds = Milliseconds + period1.Milliseconds;

        if (milliseconds >= 1000)
        {
            totalSeconds += milliseconds / 1000;
            milliseconds = milliseconds % 1000;
        }

        return new TimePeriod(totalSeconds, milliseconds);
    }

    /// <summary>
    /// Adds two <see cref="TimePeriod"/> objects and returns a new <see cref="TimePeriod"/> representing the sum of the durations.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to add.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to add.</param>
    /// <returns>A new <see cref="TimePeriod"/> object representing the sum of <paramref name="period1"/> and <paramref name="period2"/>.</returns>
    public static TimePeriod Plus(TimePeriod period1, TimePeriod period2)
    {
        return period1.Adding(period2);
    }

    /// <summary>
    /// Adds two <see cref="TimePeriod"/> objects and returns a new <see cref="TimePeriod"/> representing the sum of the durations.
    /// </summary>
    /// <param name="period1">The first <see cref="TimePeriod"/> object to add.</param>
    /// <param name="period2">The second <see cref="TimePeriod"/> object to add.</param>
    /// <returns>A new <see cref="TimePeriod"/> object representing the sum of <paramref name="period1"/> and <paramref name="period2"/>.</returns>
    public static TimePeriod operator +(TimePeriod period1, TimePeriod period2)
    {
        return Plus(period1, period2);
    }
    #endregion

    #region Substracting
    private TimePeriod Substracting(TimePeriod period1)
    {
        long subtractedTotalSeconds = TotalSeconds - period1.TotalSeconds;
        int milliseconds = Milliseconds - period1.Milliseconds;

        if (milliseconds < 0)
        {
            subtractedTotalSeconds--;
            milliseconds += 1000;
        }

        return subtractedTotalSeconds > 0 ? new TimePeriod(subtractedTotalSeconds, milliseconds) : new TimePeriod(0);
    }
    /// <summary>
    /// Subtracts two <see cref="TimePeriod"/> objects and returns a new <see cref="TimePeriod"/> representing the difference between the durations.
    /// </summary>
    /// <param name="period1">The <see cref="TimePeriod"/> to subtract from.</param>
    /// <param name="period2">The <see cref="TimePeriod"/> to subtract.</param>
    /// <returns>A new <see cref="TimePeriod"/> object representing the difference between <paramref name="period1"/> and <paramref name="period2"/>.</returns>
    public static TimePeriod Minus(TimePeriod period1, TimePeriod period2)
    {
        return period1.Substracting(period2);
    }

    /// <summary>
    /// Subtracts two <see cref="TimePeriod"/> objects and returns a new <see cref="TimePeriod"/> representing the difference between the durations.
    /// </summary>
    /// <param name="period1">The <see cref="TimePeriod"/> to subtract from.</param>
    /// <param name="period2">The <see cref="TimePeriod"/> to subtract.</param>
    /// <returns>A new <see cref="TimePeriod"/> object representing the difference between <paramref name="period1"/> and <paramref name="period2"/>.</returns>
    public static TimePeriod operator -(TimePeriod period1, TimePeriod period2)
    {
        return Minus(period1, period2);
    }
    #endregion
}