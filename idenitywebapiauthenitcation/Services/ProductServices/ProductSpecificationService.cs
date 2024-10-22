using Data;
using EccomerceApi.Interfaces.ProductInterfaces;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EccomerceApi.Services.ProductServices
{
    public class ProductSpecificationService : IProductSpecification
    {
        private readonly AppDbContext _identityDbContext;

        public ProductSpecificationService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<ProductSpecificationViewModel> CreateAsync(int productId, ProductSpecificationViewModel productSpecification)
        {
            using var transaction = await _identityDbContext.Database.BeginTransactionAsync();

            try
            {
                var productSpec = new ProductSpecification
                {
                    ProductId = productId,
                    Color = productSpecification.Color,
                    Sensor = productSpecification.Sensor,
                    ModelNumber = productSpecification.ModelNumber,
                    ProcessorSpeed = productSpecification.ProcessorSpeed,
                    ScreenSize = productSpecification.ScreenSize,
                    ScreenResolution = productSpecification.ScreenResolution,
                    ScreenTechnology = productSpecification.ScreenTechnology,
                    RearCameraResolution = productSpecification.RearCameraResolution,
                    FrontCameraResolution = productSpecification.FrontCameraResolution,
                    RAM = productSpecification.RAM,
                    InternalStorage = productSpecification.InternalStorage,
                    SimType = productSpecification.SimType,
                    SimCount = productSpecification.SimCount,
                    NFC = productSpecification.NFC,
                    BluetoothVersion = productSpecification.BluetoothVersion,
                    UsbInterface = productSpecification.UsbInterface,
                    OperatingSystem = productSpecification.OperatingSystem,
                    BatteryCapacity = productSpecification.BatteryCapacity,
                    Waterproof = productSpecification.Waterproof,
                    WaterResistanceRating = productSpecification.WaterResistanceRating,
                    SplashResistant = productSpecification.SplashResistant
                };

                _identityDbContext.ProductSpecifications.Add(productSpec);
                await _identityDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                productSpecification.ProductId = productSpec.ProductId;
                return productSpecification;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productSpecification = await _identityDbContext.ProductSpecifications
                            .FirstOrDefaultAsync(p => p.Id == id);

            if (productSpecification != null)
            {
                _identityDbContext.ProductSpecifications.Remove(productSpecification);
                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ProductSpecificationViewModel> SearchByProductIdAsync(int productId)
        {
            var productSpecification = await _identityDbContext.ProductSpecifications
                            .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (productSpecification == null)
            {
                return null;
            }

            return new ProductSpecificationViewModel
            {
                ProductId = productSpecification.ProductId,
                Color = productSpecification.Color,
                Sensor = productSpecification.Sensor,
                ModelNumber = productSpecification.ModelNumber,
                ProcessorSpeed = productSpecification.ProcessorSpeed,
                ScreenSize = productSpecification.ScreenSize,
                ScreenResolution = productSpecification.ScreenResolution,
                ScreenTechnology = productSpecification.ScreenTechnology,
                RearCameraResolution = productSpecification.RearCameraResolution,
                FrontCameraResolution = productSpecification.FrontCameraResolution,
                RAM = productSpecification.RAM,
                InternalStorage = productSpecification.InternalStorage,
                SimType = productSpecification.SimType,
                SimCount = productSpecification.SimCount,
                NFC = productSpecification.NFC,
                BluetoothVersion = productSpecification.BluetoothVersion,
                UsbInterface = productSpecification.UsbInterface,
                OperatingSystem = productSpecification.OperatingSystem,
                BatteryCapacity = productSpecification.BatteryCapacity,
                Waterproof = productSpecification.Waterproof,
                WaterResistanceRating = productSpecification.WaterResistanceRating,
                SplashResistant = productSpecification.SplashResistant
            };
        }

        public async Task<bool> UpdateAsync(int productId, ProductSpecificationViewModel productSpecification)
        {
            var existingProductSpecification = await _identityDbContext.ProductSpecifications
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (existingProductSpecification != null)
            {
                existingProductSpecification.Color = productSpecification.Color;
                existingProductSpecification.Sensor = productSpecification.Sensor;
                existingProductSpecification.ModelNumber = productSpecification.ModelNumber;
                existingProductSpecification.ProcessorSpeed = productSpecification.ProcessorSpeed;
                existingProductSpecification.ScreenSize = productSpecification.ScreenSize;
                existingProductSpecification.ScreenResolution = productSpecification.ScreenResolution;
                existingProductSpecification.ScreenTechnology = productSpecification.ScreenTechnology;
                existingProductSpecification.RearCameraResolution = productSpecification.RearCameraResolution;
                existingProductSpecification.FrontCameraResolution = productSpecification.FrontCameraResolution;
                existingProductSpecification.RAM = productSpecification.RAM;
                existingProductSpecification.InternalStorage = productSpecification.InternalStorage;
                existingProductSpecification.SimType = productSpecification.SimType;
                existingProductSpecification.SimCount = productSpecification.SimCount;
                existingProductSpecification.NFC = productSpecification.NFC;
                existingProductSpecification.BluetoothVersion = productSpecification.BluetoothVersion;
                existingProductSpecification.UsbInterface = productSpecification.UsbInterface;
                existingProductSpecification.OperatingSystem = productSpecification.OperatingSystem;
                existingProductSpecification.BatteryCapacity = productSpecification.BatteryCapacity;
                existingProductSpecification.Waterproof = productSpecification.Waterproof;
                existingProductSpecification.WaterResistanceRating = productSpecification.WaterResistanceRating;
                existingProductSpecification.SplashResistant = productSpecification.SplashResistant;

                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
