using MudBlazor;

namespace PI6.Components.Shared;

public partial class AdvCarousel
{
    private readonly bool arrows = true;
    private readonly bool bullets = true;
    //private readonly bool enableSwipeGesture = true;
    private readonly bool autocycle = true;
    private readonly Transition transition = Transition.Slide;

    private readonly string _path1 = @"/files/c1.png";
    private readonly string _path2 = @"/files/c2.png";
}