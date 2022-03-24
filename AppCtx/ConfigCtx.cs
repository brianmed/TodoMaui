namespace TodoMaui.AppCtx;

public static class Options
{
    public static string AppName
    {
        get
        {
            return nameof(TodoMaui);
        }
    }

    public static string AppDirectory
    {
        get
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), AppName);
        }
    }

    public static string LogDirectory
    {
        get
        {
            return Path.Combine(AppDirectory, "Log");
        }
    }

    public static string AppDbDirectory
    {
        get
        {
            return Path.Combine(AppDirectory, "AppDb", nameof(TodoMaui));
        }
    }

    public static string AppDbFile
    {
        get
        {
            return Path.Combine(AppDbDirectory, $"{nameof(TodoMaui)}.sqlite");
        }
    }

    public static string AppDbConnectionString
    {
        get
        {
            return $"Data Source={AppDbFile};";
        }
    }

    public static string ImapAccountDbDirectory(long imapAccountId)
    {
        return Path.Combine(AppDirectory, "ImapAccountDb", imapAccountId.ToString(), "Db");
    }
}
