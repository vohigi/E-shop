using System.Collections.Generic;
using EShop.Data.Entities;

namespace EShop.Data.Helpers
{
    public static class TextHelper
    {
        private const char dotChar = '•';
        public static string ConvertToFullName(string firstName, string secondName, string lastName)
        {
            return $"{lastName}{ExistingName(firstName)}{ExistingName(secondName)}";
        }

        public static string GetShortString(string text, int length = 20)
        {
            return !string.IsNullOrEmpty(text) && text.Length > length
                ? text.Substring(0, length) + "..." 
                : text ?? "";
        }

        public static string GetSpecificationsString(ProductEntity productEntity)
        {
            return $"{(productEntity.NumberOfSimCards.HasValue ? $"{productEntity.NumberOfSimCards} SIM {dotChar} " : "")}" +
               $"{(productEntity.DisplayDiagonal.HasValue ? $"Екран: {productEntity.DisplayDiagonal}\'\' {dotChar} " : "")}" +
               $"{(productEntity.DisplayType.HasValue ? $"{productEntity.DisplayType} {dotChar} " : "")}" +
               $"{(productEntity.DisplayWidth.HasValue && productEntity.DisplayHeight.HasValue ? $"{productEntity.DisplayWidth}x{productEntity.DisplayHeight} {dotChar} " : "")}" +
               $"{(productEntity.Rom.HasValue ? $"Внутрішня пам'ять: {productEntity.Rom} ГБ {dotChar} " : "")}" +
               $"{(productEntity.Ram.HasValue ? $"Оперативна пам'ять: {productEntity.Ram} ГБ {dotChar} " : "")}" +
               $"{(!string.IsNullOrEmpty(productEntity.ProcessorName) ? $"Процесор: {productEntity.ProcessorName} {dotChar} " : "")}" +
               $"{(productEntity.NumberOfCores.HasValue ? $"Кількість ядер: {productEntity.NumberOfCores} {dotChar} " : "")}" +
               $"{(productEntity.Clock.HasValue ? $"Частота, GHz: {productEntity.Clock} {dotChar} " : "")}" +
               $"{(!string.IsNullOrEmpty(productEntity.OperatingSystem) ? $"ОС: {productEntity.OperatingSystem} {dotChar} " : "")}" +
               $"{(productEntity.BatteryCapacity.HasValue ? $"Ємність акумулятора: {productEntity.BatteryCapacity} мАгод {dotChar} " : "")}" +
               $"{(!string.IsNullOrEmpty(productEntity.FrontCamera) ? $"Фронтальна камера: {productEntity.FrontCamera} {dotChar} " : "")}" +
               $"{(!string.IsNullOrEmpty(productEntity.RearCamera) ? $"Основна камера: {productEntity.RearCamera} {dotChar} " : "")}" +
               $"{(productEntity.Width.HasValue ? $"Ширина корпусу, мм: {productEntity.Width} {dotChar} " : "")}" +
               $"{(productEntity.Height.HasValue ? $"Висота корпусу, мм: {productEntity.Height} {dotChar} " : "")}" +
               $"{(productEntity.Weight.HasValue ? $"Вага корпусу, г: {productEntity.Weight} {dotChar} " : "")}" +
               $"{(productEntity.HasSdCardSlot ? $"Присутній лоток для SD-карти {dotChar} " : $"Лоток для SD-карти відсутній {dotChar} ")}" +
               $"{(productEntity.HasNFC ? $"Присутній модуль NFC" : $"Модуль NFC відсутній")}"
                ;
        }
        public static Dictionary<string, string> PrepareSpecifications(ProductEntity pe) => new Dictionary<string, string>()
        {
            {"Виробник:", string.IsNullOrEmpty(pe.Manufacturer) ? "" : pe.Manufacturer},
            {"ОС:",!string.IsNullOrEmpty(pe.OperatingSystem) ? pe.OperatingSystem : "-"},
            {"Кількість сім-карток:", pe.NumberOfSimCards.HasValue ? pe.NumberOfSimCards.ToString() : "-"},
            {"NFC",pe.HasNFC ? "+" : "-"},
            {"Можливість встановлення SD",pe.HasSdCardSlot ? "+" : "-"},
            {"Тип дисплею:", pe.DisplayType.HasValue ? pe.DisplayType.ToString() : "-"},
            {"Діагональ дисплею:", pe.DisplayDiagonal.HasValue ? $"{pe.DisplayDiagonal}''" : "-"},
            {"Роздільна здатність дисплею:", pe.DisplayWidth.HasValue && pe.DisplayHeight.HasValue ? $"{pe.DisplayWidth}x{pe.DisplayHeight}" : "-" },
            {"Процесор:",!string.IsNullOrEmpty(pe.ProcessorName) ? pe.ProcessorName : "-"},
            {"Кількість ядер:", pe.NumberOfCores.HasValue ? pe.NumberOfCores.ToString() : "-"},
            {"Частота, GHz:", pe.Clock.HasValue ? pe.Clock.ToString() : "-"},
            {"Внутрішня пам'ять:", pe.Rom.HasValue ? $"{pe.Rom} ГБ":"-"},
            {"Оперативна пам'ять:",pe.Ram.HasValue ? $"{pe.Ram} ГБ":"-"},
            {"Ємність акумулятора:",pe.BatteryCapacity.HasValue ? $"{pe.BatteryCapacity} мАгод" : "-"},
            {"Фронтальна камера:",!string.IsNullOrEmpty(pe.FrontCamera) ? pe.FrontCamera : "-"},
            {"Основна камера:",!string.IsNullOrEmpty(pe.RearCamera) ? pe.RearCamera : "-"},
            {"Ширина корпусу, мм:", pe.Width.HasValue ? pe.Width.ToString() : "-"},
            {"Висота корпусу, мм:", pe.Height.HasValue ? pe.Height.ToString() : "-"},
            {"Вага корпусу, г:", pe.Weight.HasValue ? pe.Weight.ToString() : "-"},
        };
        private static string ExistingName(string name)
        {
            return string.IsNullOrEmpty(name) ? "" : $" {name}";
        }
        
    }
}