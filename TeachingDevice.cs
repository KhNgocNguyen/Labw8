using System;

namespace DeviceMonitoringSystem
{
    /******************************
    //MSSV: 202418955
    //Họ tên: Nguyễn Khánh Ngọc
    ******************************/
    public abstract class TeachingDevice
    {
        public string Id { get; }
        public string Name { get; }
        public DeviceStatus Status { get; private set; }

        public event EventHandler<DeviceStatusChangedEventArgs>? StatusChanged;
        public event EventHandler<DeviceAlertEventArgs>? AlertRaised;

        protected TeachingDevice(string id, string name)
        {
            Id = id;
            Name = name;
            Status = DeviceStatus.Available;
        }

        public void ChangeStatus(DeviceStatus newStatus)
        {
            DeviceStatus oldStatus = Status;
            Status = newStatus;

            OnStatusChanged(new DeviceStatusChangedEventArgs(
                Id,
                oldStatus,
                newStatus,
                DateTime.Now));
        }

        protected void RaiseAlert(string message, AlertSeverity severity)
        {
            OnAlertRaised(new DeviceAlertEventArgs(
                Id,
                message,
                severity,
                DateTime.Now));
        }

        protected virtual void OnStatusChanged(DeviceStatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        protected virtual void OnAlertRaised(DeviceAlertEventArgs e)
        {
            AlertRaised?.Invoke(this, e);
        }
    }
}