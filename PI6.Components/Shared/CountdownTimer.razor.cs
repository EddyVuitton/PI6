using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Components.Objects;
using System.Timers;

namespace PI6.Components.Shared;

public partial class CountdownTimer
{
    //https://www.youtube.com/watch?v=gvXn4KdB-eE

    [Inject] public IJSRuntime JSRuntime { get; set; }
    [Parameter] public AppState AppState { get; set; }
    [Parameter] public Action<bool> OnDisableChange { get; set; }

    private const string _DEFAULT_TIME = "00:00:00";
    private string _elapsedTime = string.Empty;
    private System.Timers.Timer _timer = new(1);
    private DateTime _startDateTime = DateTime.Now;

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        DateTime currentTime = e.SignalTime;
        var newElapsedTime = _startDateTime.Subtract(currentTime);
        _elapsedTime = $"{newElapsedTime}"[..8];

        if (newElapsedTime.TotalSeconds <= 0)
        {
            _timer.Enabled = false;
            _elapsedTime = _DEFAULT_TIME;
            OnDisableChange?.Invoke(true);
        }

        InvokeAsync(StateHasChanged);
    }

    protected override void OnInitialized()
    {
        if (AppState.RequiredTime > 0)
        {
            _elapsedTime = GetFormattedTime(AppState.RequiredTime);
            StartTimer();
        }
        else
            _elapsedTime = _DEFAULT_TIME;
    }

    void StartTimer()
    {
        _startDateTime = _startDateTime.AddSeconds(AppState.RequiredTime);
        _timer = new System.Timers.Timer(1)
        {
            AutoReset = true,
            Enabled = true
        };
        _timer.Elapsed += OnTimedEvent;
    }

    private static string GetFormattedTime(int seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

        return new string($"{timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}");
    }
}