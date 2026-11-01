using System;
using System.Collections.Generic;

namespace DeviceMonitoringSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var projectorRemote = new ProjectorRemote("PR-01", "Remote Máy Chiếu P.101");
            var acRemote = new AirConditionerRemote("AC-01", "Remote Điều Hòa", "P.101");
            var mic = new WirelessMicrophone("MIC-01", "Micro Không Dây P.101");

            var devices = new List<TeachingDevice> { projectorRemote, acRemote, mic };

            var notificationService = new ConsoleNotificationService();
            var auditLogger = new DeviceAuditLogger();
            var maintenanceService = new MaintenanceService();

            using var coordinator = new DeviceMonitoringCoordinator(
                devices, notificationService, auditLogger, maintenanceService);

            coordinator.Subscribe();

            // Đăng ký thêm bộ lọc tùy chỉnh (FilteredAlertSubscriber)
            var criticalOnly = new FilteredAlertSubscriber(
                alert => alert.Severity == AlertSeverity.Critical,
                alert => Console.WriteLine($"---> [CRITICAL FILTER]: {alert.DeviceId} - {alert.Message}"));

            mic.AlertRaised += criticalOnly.HandleAlert;

            Console.WriteLine("=== 1. KIỂM TRA THAY ĐỔI TRẠNG THÁI ===");
            projectorRemote.ChangeStatus(DeviceStatus.Borrowed);
            projectorRemote.ChangeStatus(DeviceStatus.Borrowed); // Sẽ lộ Bug 1 ở đây

            Console.WriteLine("\n=== 2. KIỂM TRA CẢNH BÁO THIẾT BỊ ===");
            projectorRemote.UpdateBatteryLevel(15); // Sẽ lộ Bug 2 ở đây
            acRemote.UpdateResponseState(false);
            mic.UpdateSignalQuality(30);
            mic.UpdateBatteryLevel(0);

            Console.WriteLine("\n=== 3. KIỂM TRA NGOẠI LỆ ===");
            try
            {
                mic.UpdateBatteryLevel(150);
            }
            catch (InvalidDeviceReadingException ex)
            {
                Console.WriteLine($"[Exception Caught] Thiết bị {ex.DeviceId} nhận giá trị sai: {ex.InvalidValue} ({ex.Message})");
            }

            Console.WriteLine("\n=== 4. LỊCH SỬ AUDIT LOG ===");
            foreach (var entry in auditLogger.Entries)
            {
                Console.WriteLine(entry);
            }

            Console.WriteLine("\n=== 5. DANH SÁCH YÊU CẦU BẢO TRÌ ===");
            foreach (var req in maintenanceService.Requests)
            {
                Console.WriteLine(req);
            }
        }
    }
}