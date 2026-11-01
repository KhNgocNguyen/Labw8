/****************/  
//MSSV: 202418955
//Họ tên: Nguyễn Khánh Ngọc 
/****************/
using System;
namespace DeviceMonitoringSystem
{
    public class InvalidDeviceReadingException : Exception
    {
        public string DeviceId { get; }
        public int InvalidValue { get; }

        public InvalidDeviceReadingException(string deviceId, int invalidValue, string message)
            : base(message)
        {
            DeviceId = deviceId;
            InvalidValue = invalidValue;
        }
    }
}