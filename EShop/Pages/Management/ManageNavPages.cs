using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Pages.Management
{
    public static class ManageNavPages
    {
        public static string Products => "Products";
        public static string CreateProducts => "CreateProducts";
        public static string CreateAdministrator => "CreateAdministrator";
        
        public static string ProductsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Products);
        public static string CreateProductsNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateProducts);
        public static string CreateAdministratorNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateAdministrator);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
