using System.Linq;
using EShop.Data;
using EShop.Data.Entities;

namespace EShop.Utilities
{
    public class ShippingTypesInitializer
    {
        public static void ShippingInitialize(ShopContext shopContext)
        {

            shopContext.Database.EnsureCreated();
            
            //stop initializing if administrator role already exists
            if(shopContext.ShippingTypes.Any()) return;
            
            //create role
            shopContext.ShippingTypes.Add(new ShippingTypeEntity()
            {
                Name = "Самовивіз з магазину",
                Description = "Самовивіз за адресою вул. Воздвиженська, 123. Графік роботи: з 02.00 до 05.00"
            });
            shopContext.ShippingTypes.Add(new ShippingTypeEntity()
            {
                Name = "Нова пошта",
                Description = "Вартість доставаки уточнюйте у менеджера"
            });
            //create user for that role 

            shopContext.SaveChanges();
        }
    }
}