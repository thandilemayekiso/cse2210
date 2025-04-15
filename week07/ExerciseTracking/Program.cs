using System;
using System.Collections.Generic;

abstract class Activity
{
    private string _date;
    private int _minutes;

    public Activity(string date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public string GetDate() => _date;
    public int GetMinutes() => _minutes;

    public abstract double GetDistance(); // in km
    public abstract double GetSpeed();    // in kph
    public abstract double GetPace();     // min per km

    public virtual string GetSummary()
    {
        return $"{_date} {GetType().Name} ({_minutes} min): " +
               $"Distance {GetDistance():0.0} km, Speed: {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

class Running : Activity
{
    private double _distanceKm;

    public Running(string date, int minutes, double distanceKm)
        : base(date, minutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;

    public override double GetSpeed() => (_distanceKm / GetMinutes()) * 60;

    public override double GetPace() => GetMinutes() / _distanceKm;
}

class Cycling : Activity
{
    private double _speedKph;

    public Cycling(string date, int minutes, double speedKph)
        : base(date, minutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => (_speedKph * GetMinutes()) / 60;

    public override double GetSpeed() => _speedKph;

    public override double GetPace() => 60 / _speedKph;
}

class Swimming : Activity
{
    private int _laps;

    public Swimming(string date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50) / 1000.0;

    public override double GetSpeed() => (GetDistance() / GetMinutes()) * 60;

    public override double GetPace() => GetMinutes() / GetDistance();
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 4.8),
            new Cycling("04 Nov 2022", 45, 20.0),
            new Swimming("05 Nov 2022", 40, 30)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
c:\Users\thand\OneDrive\Pictures\Screenshots\Screenshot 2025-04-15 231111.png