namespace TimeAndTimePeriod;

public struct TimePeriod
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
}