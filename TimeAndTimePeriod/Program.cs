namespace TimeAndTimePeriod;

class Program
{
    static void Main(string[] args)
    {
        var time1 = new Time(5, 55, 55);
        var time2 = new Time(5, 55, 55);
        var time3 = new Time(5);
        var time4 = new Time(5);
        var time5 = new Time(5, 2);
        var time6 = new Time("14:55:50");
        
        Console.WriteLine(time1.Equals(time2));
        Console.WriteLine(time3.Equals(time4));
        Console.WriteLine(time3.Equals(time2));

        Console.WriteLine();

        Console.WriteLine($"{time1 > time2} false");
        Console.WriteLine($"{time1 < time3} false");
        Console.WriteLine($"{time1 > time3} true");
        Console.WriteLine($"{time1 <= time2} true");
        Console.WriteLine($"{time1 >= time2} true");
        Console.WriteLine($"{time1 < time2} false");

        Console.WriteLine();

        Console.WriteLine(time5.ToString());
        Console.WriteLine(time6.ToString());

        var time7 = new Time(23, 59);
        var timePeriod3 = new TimePeriod(25, 2);
        Console.WriteLine(time7 + timePeriod3);
        Console.WriteLine(Time.Plus(time7, timePeriod3));

        Console.WriteLine();
        Console.WriteLine("TIME PERIOD:");

        var timePeriod1 = new TimePeriod(100, 55, 59);
        var timePeriod2 = new TimePeriod("99:04:1");
        
        Console.WriteLine(timePeriod1);
        Console.WriteLine(timePeriod2);

        Console.WriteLine();

        Console.WriteLine(timePeriod1 + timePeriod2);
        Console.WriteLine(timePeriod1 - timePeriod2);
        Console.WriteLine(TimePeriod.Plus(timePeriod1, timePeriod2));

        var timePeriod10 = new TimePeriod(23);
        
        Console.WriteLine(time1.Minus(timePeriod10));
        
        
        Console.WriteLine(TimePeriod.Minus(timePeriod1,timePeriod10));
    }
}