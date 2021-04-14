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
               $"{(productEntity.Rom.HasValue ? $"Внутрішня пам'ять: {productEntity.Rom} {dotChar} " : "")}" +
               $"{(productEntity.Ram.HasValue ? $"Оперативна пам'ять: {productEntity.Ram} {dotChar} " : "")}" +
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
               $"{(productEntity.HasSdCardSlot ? $"Присутній лоток для сім-карти {dotChar} " : $"Лоток для сім-карти відсутній {dotChar} ")}" +
               $"{(productEntity.HasNFC ? $"Присутній модуль NFC" : $"Модуль NFC відсутній")}"
                ;
        }
        private static string ExistingName(string name)
        {
            return string.IsNullOrEmpty(name) ? "" : $" {name}";
        }
        
    }
}