namespace NuGen.Services.Interfaces
{
    public interface IConsoleHelperService
    {
        string GenerateProgress(long number, long of);
        void OverwriteLine(string message);
    }
}