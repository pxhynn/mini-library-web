namespace MiniLibrary.Web.Options
{
    public class LibrarySettings
    {
        public string AppName { get; set; } = string.Empty;
        public string SupportEmail { get; set; } = string.Empty;
        public int LowAvailableCopyThreshold { get; set; }
    }
}