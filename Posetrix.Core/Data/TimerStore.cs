using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Posetrix.Core.Data;

public class TimerStore
{
    private readonly Timer _timer;

    private int _remainingSeconds;

    public event Action<int> RemainingSecondsChanged;

    public TimerStore(int startSeconds)
    {
        _remainingSeconds = startSeconds;
        _timer = new Timer(1000); // 1 second interval.
        _timer.Elapsed += Timer_Elapsed;
    }

    public void Start() => _timer.Start();
    public void Stop() => _timer.Stop();

    public void Reset(int startSeconds)
    {
        Stop();
        _remainingSeconds = startSeconds;
        RemainingSecondsChanged?.Invoke(_remainingSeconds);
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Debug.WriteLine(_remainingSeconds);

        if (_remainingSeconds > 0)
        {
            _remainingSeconds--;
            RemainingSecondsChanged?.Invoke(_remainingSeconds);
        }
        else
        {
            Stop();
        }

    }
}
