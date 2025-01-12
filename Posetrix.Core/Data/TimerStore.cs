using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Posetrix.Core.Data;

public partial class TimerStore : ObservableObject
{
    private readonly Timer _timer;
    private TimeSpan _timeElapsed;
    private TimeSpan _timeElapsedSum = TimeSpan.Zero;

    public event Action<TimeSpan>? TimeUpdated;
    public event Action? CountdownFinished;

    private readonly Lock _timerLock = new(); // To ensure thread safety.

    [ObservableProperty] public partial bool IsTimerPaused { get; private set; }

    public TimerStore()
    {
        _timer = new Timer(1000); // 1 second interval.
        _timer.Elapsed += Timer_Elapsed;
        _timer.AutoReset = true; // Timer repeats automatically.
    }

    public void StartTimer(TimeSpan duration)
    {
        lock (_timerLock)
        {
            _timeElapsed = duration;
            IsTimerPaused = false;
            _timer.Start();
        }
    }
    public void StopTimer()
    {
        lock (_timerLock)
        {
            _timer.Stop();
        }
    }
    public void ResetTimer(TimeSpan duration)
    {
        lock (_timerLock)
        {
            _timeElapsedSum += duration - _timeElapsed;
            _timer.Stop();
            _timeElapsed = duration;
            IsTimerPaused = false;
            TimeUpdated?.Invoke(_timeElapsed);
            _timer.Start();
        }
    }

    public void PauseTimer()
    {
        lock (_timerLock)
        {
            IsTimerPaused = true;
        }
    }

    public void ResumeTimer()
    {
        lock (_timerLock)
        {
            if (IsTimerPaused)
            {
                IsTimerPaused = false;
                _timer.Start();
            }
        }
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        lock (_timerLock)
        {
            if (IsTimerPaused)
            {
                return;
            }

            if (_timeElapsed.TotalSeconds > 0)
            {
                _timeElapsed = _timeElapsed.Subtract(TimeSpan.FromSeconds(1));
                TimeUpdated?.Invoke(_timeElapsed);
            }
            else
            {
                _timer.Stop();
                CountdownFinished?.Invoke();
            }
        }
    }

    public TimeSpan GetTotalPracticeTime() => _timeElapsedSum;
    public void Dispose()
    {
        _timer.Dispose();
    }

}
