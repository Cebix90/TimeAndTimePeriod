using TimeAndTimePeriod;

namespace TimeAndTimePeriodUnitTests;


public class UnitTest1
{
    private void AssertTime(Time time, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
    {
        Assert.Equal(expectedHours, time.Hours);
        Assert.Equal(expectedMinutes, time.Minutes);
        Assert.Equal(expectedSeconds, time.Seconds);
    }

    private void AssertTime(Time time, byte expectedHours, byte expectedMinutes, byte expectedSeconds, int expectedMilliseconds)
    {
        Assert.Equal(expectedHours, time.Hours);
        Assert.Equal(expectedMinutes, time.Minutes);
        Assert.Equal(expectedSeconds, time.Seconds);
        Assert.Equal(expectedMilliseconds, time.Milliseconds);
    }

    private void AssertTimePeriod(TimePeriod period, long expectedTotalSeconds)
    {
        Assert.Equal(expectedTotalSeconds, period.TotalSeconds);
    }

    private void AssertTimePeriod(TimePeriod timePeriod, long expectedTotalSeconds, int expectedMilliseconds)
    {
        Assert.Equal(expectedTotalSeconds, timePeriod.TotalSeconds);
        Assert.Equal(expectedMilliseconds, timePeriod.Milliseconds);
    }

    [Theory]
    [InlineData(5, 55, 55,
        5, 55, 55)]
    [InlineData(23, 5, 4,
        23, 5, 4)]
    public void ConstructorTime_3params(byte a, byte b, byte c,
        byte expectedA, byte expectedB, byte expectedC)
    {
        var time = new Time(a, b, c);

        AssertTime(time, expectedA, expectedB, expectedC);
    }

    [Theory]
    [InlineData(5, 55,
        5, 55, 0)]
    [InlineData(23, 5,
        23, 5, 0)]
    public void ConstructorTime_2params(byte a, byte b,
        byte expectedA, byte expectedB, byte expectedC)
    {
        var time = new Time(a, b);

        AssertTime(time, expectedA, expectedB, expectedC);
    }

    [Theory]
    [InlineData(5,
        5, 0, 0)]
    [InlineData(23,
        23, 0, 0)]
    public void ConstructorTime_1param(byte a,
        byte expectedA, byte expectedB, byte expectedC)
    {
        var time = new Time(a);

        AssertTime(time, expectedA, expectedB, expectedC);
    }

    [Theory]
    [InlineData(25, 55, 2, 22)]
    [InlineData(23, 60, 1, 22)]
    [InlineData(23, 20, 61, 0)]
    [InlineData(23, 50, 1, 1002)]
    public void ConstructorTime_InvalidFormat(byte a, byte b, byte c, int d)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Time(a, b, c, d));
        Assert.Equal("Invalid time.", exception.Message);
    }

    [Theory]
    [InlineData("14:55:50",
        14, 55, 50)]
    [InlineData("10:0:20",
        10, 0, 20)]
    public void ConstructorTime_String(string a,
        byte expectedA, byte expectedB, byte expectedC)
    {
        var time = new Time(a);

        AssertTime(time, expectedA, expectedB, expectedC);
    }

    [Theory]
    [InlineData("14:55")]
    [InlineData("14:55:00:00")]
    [InlineData("14")]
    [InlineData("one:two:three")]
    public void ConstructorTime_String_InvalidFormat(string a)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Time(a));
        Assert.Equal("Invalid time format.", exception.Message);
    }

    [Theory]
    [InlineData(5, 55, 55,
        21355)]
    [InlineData(23, 5, 4,
        83104)]
    public void ConstructorTimePeriod_3params(int a, byte b, byte c,
        long expectedA)
    {
        var timePeriod = new TimePeriod(a, b, c);

        AssertTimePeriod(timePeriod, expectedA);
    }

    [Theory]
    [InlineData(5, 55,
        21300)]
    [InlineData(23, 5,
        83100)]
    public void ConstructorTimePeriod_2params(int a, byte b,
        long expectedA)
    {
        var timePeriod = new TimePeriod(a, b);

        AssertTimePeriod(timePeriod, expectedA);
    }

    [Theory]
    [InlineData(5,
        18000)]
    [InlineData(23,
        82800)]
    public void ConstructorTimePeriod_1param(int a,
        long expectedA)
    {
        var timePeriod = new TimePeriod(a);

        AssertTimePeriod(timePeriod, expectedA);
    }

    [Theory]
    [InlineData(-1, 55, 2, 22)]
    [InlineData(23, 60, 1, 22)]
    [InlineData(23, 20, 61, 0)]
    [InlineData(23, 50, 1, 1002)]
    [InlineData(23, 50, 1, -1002)]
    public void ConstructorTimePeriod_InvalidFormat(int a, byte b, byte c, int d)
    {
        var exception = Assert.Throws<ArgumentException>(() => new TimePeriod(a, b, c, d));
        if (a < 0 || b < 0 || c < 0 || d < 0)
        Assert.Equal("Invalid time period.", exception.Message);
        else
        Assert.Equal("Invalid time.", exception.Message);
    }

    [Theory]
    [InlineData("5:55:55:123",
        21355, 123)]
    [InlineData("2:1:12:500",
        7272, 500)]
    public void ConstructorTimePeriod_String(string a, long expectedTotalSeconds, int expectedMilliseconds)
    {
        var timePeriod = new TimePeriod(a);

        AssertTimePeriod(timePeriod, expectedTotalSeconds, expectedMilliseconds);
    }

    [Theory]
    [InlineData("14:55")]
    [InlineData("14")]
    [InlineData("one:two:three")]
    public void ConstructorTimePeriod_String_InvalidFormat(string a)
    {
        var exception = Assert.Throws<ArgumentException>(() => new TimePeriod(a));
        Assert.Equal("Invalid time format.", exception.Message);
    }

    [Theory]
    [InlineData(5, 55, 55,
        "05:55:55")]
    [InlineData(2, 1, 12,
        "02:01:12")]
    public void ToString_Time(byte a, byte b, byte c,
        string expectedA)
    {
        var time = new Time(a, b, c);

        Assert.Equal(time.ToString(), expectedA);
    }

    [Theory]
    [InlineData(512, 55, 55,
        "512:55:55")]
    [InlineData(2, 1, 12,
        "2:01:12")]
    public void ToString_TimePeriod(int a, byte b, byte c,
        string expectedA)
    {
        var timePeriod = new TimePeriod(a, b, c);

        Assert.Equal(timePeriod.ToString(), expectedA);
    }

    [Fact]
    public void Time_EqualityAndComparison()
    {
        var time1 = new Time(10, 30, 45);
        var time2 = new Time(10, 30, 45);
        var time3 = new Time(8, 15, 20);

        Assert.True(time1 == time2);
        Assert.False(time1 != time2);
        Assert.True(time1.Equals(time2));
        Assert.Equal(0, time1.CompareTo(time2));

        Assert.False(time1 == time3);
        Assert.True(time1 != time3);
        Assert.False(time1.Equals(time3));
        Assert.True(time1 > time3);
        Assert.True(time1 >= time3);
        Assert.False(time1 < time3);
        Assert.False(time1 <= time3);
    }

    [Fact]
    public void Time_ArithmeticOperations()
    {
        var time1 = new Time(10, 30);
        var period1 = new TimePeriod(3, 45, 15);
        var period2 = new TimePeriod(14, 45, 15);

        var result1 = time1 + period1;
        var result2 = time1 + period2;
        var result3 = Time.Plus(time1, period2);
        var result4 = Time.Minus(time1, period1);
        var result5 = time1 - period2;

        Assert.Equal(new Time(14, 15, 15), result1);
        Assert.Equal(new Time(1, 15, 15), result2);
        Assert.Equal(new Time(1, 15, 15), result3);
        Assert.Equal(new Time(6, 44, 45), result4);
        Assert.Equal(new Time(19, 44, 45), result5);
    }

    [Fact]
    public void TimePeriod_EqualityAndComparison()
    {
        var period1 = new TimePeriod(10, 30, 45);
        var period2 = new TimePeriod(10, 30, 45);
        var period3 = new TimePeriod(8, 15, 20);

        Assert.True(period1 == period2);
        Assert.False(period1 != period2);
        Assert.True(period1.Equals(period2));
        Assert.Equal(0, period1.CompareTo(period2));

        Assert.False(period1 == period3);
        Assert.True(period1 != period3);
        Assert.False(period1.Equals(period3));
        Assert.True(period1 > period3);
        Assert.True(period1 >= period3);
        Assert.False(period1 < period3);
        Assert.False(period1 <= period3);
    }

    [Fact]
    public void TimePeriod_ArithmeticOperations()
    {
        var period1 = new TimePeriod(3, 45, 15);
        var period2 = new TimePeriod(14, 45, 15);
        var period3 = new TimePeriod(100);

        var result1 = period1 + period2;
        var result2 = result1 + period3;
        var result3 = TimePeriod.Plus(result2, period3);
        var result4 = result3 - result2;
        var result5 = TimePeriod.Minus(result4, period1);

        Assert.Equal(new TimePeriod(18, 30, 30), result1);
        Assert.Equal(new TimePeriod(118, 30, 30), result2);
        Assert.Equal(new TimePeriod(218, 30, 30), result3);
        Assert.Equal(new TimePeriod(100), result4);
        Assert.Equal(new TimePeriod(96, 14, 45), result5);
    }
}