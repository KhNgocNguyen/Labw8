namespace DeviceMonitoringSystem
{
    public class AirConditionerRemote : TeachingDevice
    {
        public string AssignedRoom { get; }
        public bool IsResponsive { get; private set; } = true;

        public AirConditionerRemote(string id, string name, string assignedRoom) : base(id, name)
        {
            AssignedRoom = assignedRoom;
        }

        public void UpdateResponseState(bool isResponsive)
        {
            bool oldState = IsResponsive;
            IsResponsive = isResponsive;

            // Chỉ phát cảnh báo khi chuyển từ phản hồi (true) sang không phản hồi (false)
            if (oldState && !isResponsive)
            {
                RaiseAlert($"Điều khiển điều hòa phòng {AssignedRoom} mất phản hồi!", AlertSeverity.Critical);
            }
        }
    }
}