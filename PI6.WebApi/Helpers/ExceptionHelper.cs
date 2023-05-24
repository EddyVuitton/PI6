using static System.Console;

namespace PI6.WebApi.Helpers;

public static class ExceptionHelper
{
    public static void PrintException(Exception e)
    {
        WriteLine($"{e.Message}");
        WriteLine($"Source: {e.Source}");
    }
}