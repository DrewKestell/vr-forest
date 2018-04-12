using UnityEngine;

public class Timer : MonoBehaviour
{
    public int TimeScale;

    const int secondsPerDay = 86400;
    const int minutesPerDay = 1440;
    const int hoursPerDay = 24;
    const int secondsPerHour = 3600;
    const int minutesPerHour = 60;
    const int secondsPerMinute = 60;
    
    public int DayCount { get; private set; }
    public int HourCount { get; private set; }
    public int MinuteCount { get; private set; }
    public int SecondCount { get; private set; }

    int offset;

    void Start()
    {
        offset = (86400 / (TimeScale * 8)) * 3;
    }

    void Update()
    {
        var currentTime = (Time.time + offset) * TimeScale;

        DayCount = (int)(currentTime / secondsPerDay);
        HourCount = (int)(currentTime / secondsPerHour) - (DayCount * hoursPerDay);
        MinuteCount = (int)(currentTime / secondsPerMinute) - (HourCount * minutesPerHour) - (DayCount * minutesPerDay);
        SecondCount = (int)currentTime - (MinuteCount * secondsPerMinute) - (HourCount * secondsPerHour) - (DayCount * secondsPerDay);
    }

    public string GetTime() => $"Day {DayCount + 1}, {HourCount:D2}:{MinuteCount:D2}:{SecondCount:D2}";
}
