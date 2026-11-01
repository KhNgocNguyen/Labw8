using System;

namespace DeviceMonitoringSystem
{
    public class ConsoleNotificationService
    {
        public void HandleAlert(object? sender, DeviceAlertEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] {e.DeviceId}: {e.Message}");
        }
    }
}