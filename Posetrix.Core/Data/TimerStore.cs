using System.Timers;
using Timer = System.Timers.Timer;

namespace Posetrix.Core.Data;

public class TimerStore
{
    private readonly Timer _timer;

    private int _seconds;
    private readonly int _startSeconds;

    public event Action<TimeSpan>? TimeUpdated;

    public TimerStore(int seconds)
    {
        _seconds = seconds;
        _startSeconds = seconds;

        _timer = new Timer(1000); // 1 second interval.
        _timer.Elapsed += Timer_Elapsed;
    }

    public void Start() => _timer.Start();
    public void Stop() => _timer.Stop();

    public void Reset()
    {
        Stop();
        var time = TimeSpan.FromSeconds(_startSeconds);
        TimeUpdated?.Invoke(time);
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (_seconds > 0)
        {
            var time = TimeSpan.FromSeconds(_seconds);
            TimeUpdated?.Invoke(time);
            _seconds--;
        }
        else
        {
            Stop();
        }

    }
}
