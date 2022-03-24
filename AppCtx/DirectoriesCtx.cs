using TodoMaui.AppCtx;

namespace TodoMaui.AppCtx;

public class DirectoriesCtx
{
    public static void Provision()
    {
        List<string> directories = new List<string>();

        directories.Add(Options.AppDirectory);
        directories.Add(Options.AppDbDirectory);

        if (IsProvisioned(directories) is false)
        {
            foreach (string directory in directories)
            {
                if (Directory.Exists(directory) is false) {
                    Directory.CreateDirectory(directory);
                }
            }
        }
    }

    private static bool IsProvisioned(List<string> directories)
    {
        foreach (string directory in directories)
        {
            if (Directory.Exists(directory) is false) {
                return false;
            }
        }

        return true;
    }
}
