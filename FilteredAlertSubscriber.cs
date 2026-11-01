using System;

namespace DeviceMonitoringSystem
{
    public class FilteredAlertSubscriber
    {
        private readonly AlertFilter filter;
        private readonly Action<DeviceAlertEventArgs> action;

        public FilteredAlertSubscriber(AlertFilter filter, Action<DeviceAlertEventArgs> action)
        {
            this.filter = filter;
            this.action = action;
        }

        public void HandleAlert(object? sender, DeviceAlertEventArgs e)
        {
            if (filter(e))
            {
                action(e);
            }
        }
    }
}