namespace TimeAndTimePeriod;

class Program
{
    static void Main(string[] args)
    {
        //Time
        var time1 = new Time(5, 55, 55);
        var time2 = new Time(5, 55, 55);
        var time3 = new Time(5);
        var time4 = new Time(5);
        var time5 = new Time(5, 2);
        var time6 = new Time("14:55:50");

        Console.WriteLine($"time1: {time1}");
        Console.WriteLine($"time2: {time2}");
        Console.WriteLine($"time3: {time3}");
        Console.WriteLine($"time4: {time4}");
        Console.WriteLine($"time5: {time5}");
        Console.WriteLine($"time6: {time6}");
        Console.WriteLine();

        Console.WriteLine($"time1.Equals(time2): {time1.Equals(time2)}");
        Console.WriteLine($"time3.Equals(time4): {time3.Equals(time4)}");
        Console.WriteLine($"time3.Equals(time2): {time3.Equals(time2)}");
        Console.WriteLine();

        Console.WriteLine($"time1 > time2: {time1 > time2}");
        Console.WriteLine($"time1 < time3: {time1 < time3}");
        Console.WriteLine($"time1 > time3: {time1 > time3}");
        Console.WriteLine($"time1 <= time2: {time1 <= time2}");
        Console.WriteLine($"time1 >= time2: {time1 >= time2}");
        Console.WriteLine($"time1 < time2: {time1 < time2}");
        Console.WriteLine();

        //TimePeriod
        var period1 = new TimePeriod(2, 30, 0);
        var period2 = new TimePeriod(900, 30);
        var period3 = new TimePeriod(0, 0, 0, 500);
        var period4 = new TimePeriod(1, 30, 0);
        var period5 = new TimePeriod(0, 30, 0);
        var period6 = new TimePeriod(0, 0, 10, 500);

        Console.WriteLine($"period1: {period1}");
        Console.WriteLine($"period2: {period2}");
        Console.WriteLine($"period3: {period3}");
        Console.WriteLine($"period4: {period4}");
        Console.WriteLine($"period5: {period5}");
        Console.WriteLine($"period6: {period6}");
        Console.WriteLine();

        Console.WriteLine($"period1.Equals(period2): {period1.Equals(period2)}");
        Console.WriteLine($"period3.Equals(period4): {period3.Equals(period4)}");
        Console.WriteLine($"period3.Equals(period2): {period3.Equals(period2)}");
        Console.WriteLine();

        Console.WriteLine($"period1 > period2: {period1 > period2}");
        Console.WriteLine($"period1 < period3: {period1 < period3}");
        Console.WriteLine($"period1 > period3: {period1 > period3}");
        Console.WriteLine($"period1 <= period2: {period1 <= period2}");
        Console.WriteLine($"period1 >= period2: {period1 >= period2}");
        Console.WriteLine($"period1 < period2: {period1 < period2}");
        Console.WriteLine();

        //Arithmetic
        Console.WriteLine("TIME ARITHMETIC");
        var result1 = time1 + period1;
        Console.WriteLine($"{time1} - {period1}  = {result1}");
        var result2 = Time.Plus(time2, period2);
        Console.WriteLine($"Time.Plus({time2}, {period2}) = {result2}");
        Console.WriteLine();

        var result3 = time1 - period1;
        Console.WriteLine($"{time1} - {period1} = {result3}");
        var result4 = Time.Minus(time2, period2);
        Console.WriteLine($"Time.Minus({time2}, {period2}) = {result4}");
        Console.WriteLine();

        Console.WriteLine("TIMEPERIOD ARITHMETIC");
        var result5 = period1 + period2;
        Console.WriteLine($"{period1} + {period2} = {result5}");
        var result6 = TimePeriod.Plus(period3, period4);
        Console.WriteLine($"TimePeriod.Plus({period3}, {period4}) = {result6}");
        Console.WriteLine();

        var result7 = period1 - period2;
        Console.WriteLine($"{period1} - {period2} = {result7}");
        var result8 = TimePeriod.Minus(period4, period5);
        Console.WriteLine($"TimePeriod.Minus({period4}, {period5}) = {result8}");
    }
}