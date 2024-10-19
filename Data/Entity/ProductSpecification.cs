namespace Data.Entity
{
    public class ProductSpecification
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Sensor { get; set; }
        public string? ModelNumber { get; set; }
        public double? ProcessorSpeed { get; set; }
        public double? ScreenSize { get; set; }
        public string? ScreenResolution { get; set; }
        public string? ScreenTechnology { get; set; }
        public string? RearCameraResolution { get; set; }
        public int? FrontCameraResolution { get; set; }
        public int? RAM { get; set; }
        public int? InternalStorage { get; set; }
        public string? SimType { get; set; }
        public int? SimCount { get; set; }
        public bool? NFC { get; set; }
        public string? BluetoothVersion { get; set; }
        public string? UsbInterface { get; set; }
        public string? OperatingSystem { get; set; }
        public int? BatteryCapacity { get; set; }
        public bool? Waterproof { get; set; }
        public string? WaterResistanceRating { get; set; }
        public bool? SplashResistant { get; set; }

        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
