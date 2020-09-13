using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    //Возможность перехода к оплате и оформлению заказа.
    //Будет использоваться для представления информации о доставке пользователю.
    //Здесь применяются атрибуты проверки достоверности из пространства имен
    //System.ComponentModel.DataAnnotations.Кроме того, в классе Order используется
    //атрибут BindNever, который предотвращает предоставление пользователем значений
    //для снабженных этим атрибутом свойств в НТТР-запросе.Такая возможность системы
    //привязки моделей, останавливает применение инфраструктурой MVC значений из
    //НТТР­запроса для заполнения конфиденциальных или важных свойств модели.
    public class Order//Заказ
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        //Создадим простой инструмент администрирования. Который позволит просматривать
        //полученные заказы и помечать их как отгруженные
        //Первым изменением, которое необходимо внести, 
        //303 является расширение модели, чтобы можно было фиксировать, какие заказы были отгружены.
        [BindNever]
        public bool Shipped { get; set; }
        //        Обновим базу данных, чтобы отразить добавление 
        //        свойства Shipped в класс Order, открыв окно командной
        //            строки или PowerShell, перейдя в папку проекта SportsStore
        //            (ту , что содержит файл Startup.cs) и выполнив следующую команду:
        ////dotnet ef migrations add ShippedOrders
        ////Миграция будет применена автоматически, когда приложение запустится и
        //в классе SeedData произойдет обращение к методу Migrate(),
        //            предоставленному инфраструктурой Entity Framework Core.


        [Required(ErrorMessage ="Please enter a name")]
        //введите имя
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter the first address line")]
        //Введите первую строку адреса
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage ="Please enter a city name")]
        //Введите название города
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a region")]
        public string Region { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage ="Please enter a country name")]
        // Введите название страны
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
