namespace NuGen.Options.Start
{
    public class StartOptions
    {
        public long? From { get; set; }
        public long? To { get; set; }
        public int NumberOfDigits { get; set; }
        public int NumbersInOneFile { get; set; } = 100;
        public string Prefix { get; set; } = "";
        public string FilePath { get; set; } = null;
    }
}