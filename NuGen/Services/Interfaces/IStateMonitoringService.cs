namespace NuGen.Services.Interfaces
{
    public interface IStateMonitoringService
    {
        void NumberGenerated();
        void NumberSaved();
        string Header { get; set; }
    }
}