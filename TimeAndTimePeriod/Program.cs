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

        Console.WriteLine();
        Console.WriteLine("TIME PERIOD:");

        var timePeriod1 = new TimePeriod(1111111, 55, 59);
        var timePeriod2 = new TimePeriod("141314:55:50");
        
        Console.WriteLine(timePeriod1.ToString());
        Console.WriteLine(timePeriod2.ToString());
    }
}