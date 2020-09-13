using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        //Добавим в класс контекста базы данных новое свойство,
        public DbSet<Order> Orders { get; set; }
        //Такого изменения достаточно для того , чтобы инфраструктура Entity FrameworkCore 
        //создала миграцию базы данных, которая позволит объектам Order сохраняться в базе 
        //    данных.Для создания миграции откроем окно командной строки или PowerShell, 
        //    перейдем в папку проекта SportsStore(где находится файл Startup. cs) и введем
        //    следующую команду: dot net ef migrations add Orders Эта команда сообщает Entity
        //    Framework Core о необходимости получить новый снимок модели данных приложения,
        //    выяснить его отличия от предыдущей версии, хранящейся в базе данных, и сгенерировать
        //    новую миграцию под названием Orders.Новая миграция будет автоматически применяться
        //        при запуске приложения, потому что в классе SeedData вызывается метод Migrate
        //        ().предоставляемый EntityFramework Core.

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");


        //}
    }
}
