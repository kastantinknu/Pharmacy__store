using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    //Мы планируем явно создать пользователя Admin, наполняя базу данных 
    //начальными данными при запуске приложения. Добавим в папку Models файл
      //  класса по имени IdentitySeedData. cs
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        //public static async void EnsurePopulated(IApplicationBuilder app)
        //{
        //    UserManager<IdentityUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
        //    //!!
        //    IdentityUser user = await userManager.FindByIdAsync(adminUser);
        //    if (user == null)
        //    {
        //        user = new IdentityUser("Admin");
        //        await userManager.CreateAsync(user, adminPassword);
        //    }
        //}
        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
        //В коде используется класс UserManager<T>, который система ASP.NET Core Identity
        //предоставляет в виде службы для управления пользователями. В базе данных производится
        //    поиск учетной записи пользователя Admin, которая в случае ее отсутствия создается
        //    ( спаролемSecret123$). Не изменяйте жестко закодированный пароль в этом примере,
        //    поскольку система Identity имеет политику проверки достоверности, которая
        //    т р е б у е т , чтобы пароли содержали цифры и диапазон символов.
    }
}
