using PI6.Components.Layout;
using System.Reflection;

namespace PI6.Server;

public partial class App
{
    private readonly List<Assembly> _additionalAssemblies = new()
    {
        typeof(MainLayout).Assembly,
    };
}