namespace DeviceMonitoringSystem
{
    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; private set; } = 100;
        public int SignalQuality { get; private set; } = 100;

        public WirelessMicrophone(string id, string name) : base(id, name) { }

        public void UpdateBatteryLevel(int newLevel)
        {
            if (newLevel < 0 || newLevel > 100)
            {
                throw new InvalidDeviceReadingException(Id, newLevel, "Mức pin micro phải từ 0 đến 100.");
            }

            int oldLevel = BatteryLevel;
            BatteryLevel = newLevel;

            if (newLevel == 0 && oldLevel != 0)
            {
                RaiseAlert("Micro không dây hết sạch pin (0%)!", AlertSeverity.Critical);
            }
            else if (newLevel < 30 && oldLevel >= 30)
            {
                RaiseAlert($"Pin micro không dây xuống thấp ({newLevel}%).", AlertSeverity.Warning);
            }
        }

        public void UpdateSignalQuality(int newQuality)
        {
            if (newQuality < 0 || newQuality > 100)
            {
                throw new InvalidDeviceReadingException(Id, newQuality, "Chất lượng tín hiệu phải từ 0 đến 100.");
            }

            int oldQuality = SignalQuality;
            SignalQuality = newQuality;

            if (newQuality == 0 && oldQuality != 0)
            {
                RaiseAlert("Micro mất hoàn toàn tín hiệu (0%)!", AlertSeverity.Critical);
            }
            else if (newQuality < 40 && oldQuality >= 40)
            {
                RaiseAlert($"Tín hiệu micro yếu ({newQuality}%).", AlertSeverity.Warning);
            }
        }
    }
}