using Microsoft.AspNetCore.Components;

namespace PI6.Components.Objects;

public class AppState : ComponentBase
{
    public bool IsDisabled { get; set; } = false;
    public int RequiredTime { get; set; } = 0;
    public event Action LoadStateChanged;

    public void SetDisable(bool newValue)
    {
        this.IsDisabled = newValue;
        LoadStateChanged.Invoke();
    }
}