namespace NuGen.Options.Start
{
    public class StartOptions
    {
        public long? From { get; set; }
        public long? To { get; set; }
        public string Prefix { get; set; } = "";
        public string FilePath { get; set; } = null;
    }
}