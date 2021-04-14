using System.Linq;
using EShop.Data;
using EShop.Data.Entities;

namespace EShop.Utilities
{
    public class PaymentInitializer
    {

        public static void PaymentInitialize(ShopContext shopContext)
        {
            
            shopContext.Database.EnsureCreated();
            
            //stop initializing if administrator role already exists
            if(shopContext.PaymentTypes.Any()) return;
            
            //create role
            shopContext.PaymentTypes.Add(new PaymentTypeEntity()
            {
                Name = "Готівкою в магазині",
                Description = ""
            });
            shopContext.PaymentTypes.Add(new PaymentTypeEntity()
            {
                Name = "Карткою онлайн",
                Description = "При оплаті карткою можлива комісія від 2% до 10% в залежності від картки і банку"
            });
            //create user for that role 

            shopContext.SaveChanges();
        }
    }
}