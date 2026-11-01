namespace DeviceMonitoringSystem
{
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; private set; } = 100;

        public ProjectorRemote(string id, string name) : base(id, name) { }

        public void UpdateBatteryLevel(int newLevel)
        {
            if (newLevel < 0 || newLevel > 100)
            {
                throw new InvalidDeviceReadingException(Id, newLevel, "Mức pin phải nằm trong khoảng từ 0 đến 100.");
            }

            int oldLevel = BatteryLevel;
            BatteryLevel = newLevel;

            if (newLevel == 0 && oldLevel != 0)
            {
                RaiseAlert("Pin điều khiển máy chiếu đã cạn kiệt (0%)!", AlertSeverity.Critical);
            }
            else if (newLevel < 20 && oldLevel >= 20)
            {
                RaiseAlert($"Pin điều khiển máy chiếu yếu ({newLevel}%).", AlertSeverity.Information);
            }
        }
    }
}