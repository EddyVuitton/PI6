using System.Diagnostics;

namespace PI6.WebApi.Helpers;

public static class ServerHelper
{
    public static string GetSolutionDirectory()
    {
        DirectoryInfo currentDirectory = new (Directory.GetCurrentDirectory());

        while (currentDirectory != null && !currentDirectory.GetFiles("*.sln").Any())
        {
            currentDirectory = currentDirectory.Parent;
        }

        return currentDirectory?.FullName;
    }

    public static string GetFilesDirectory()
    {
        bool isDebugMode = Debugger.IsAttached;

        if (isDebugMode)
        {
            var solutionDirectory = GetSolutionDirectory();

            string[] files = Directory.GetDirectories(solutionDirectory, "*", SearchOption.AllDirectories);
            return files.FirstOrDefault(x => x.Contains(@"PI6\Files"));
        }
        else
            return string.Empty;
    }
}