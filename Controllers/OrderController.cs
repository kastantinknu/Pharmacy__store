using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        //МетодList() выбирает все объекты Order в хранилище.Свойство 
        //Shipped которых имеет значение false, и передает их стандартному
        //    представлению.Метод действия List () будет использоваться для
        //    отображения администратору списка неотгруженных заказов.Метод
        //    MarkShipped () будет получать запрос POST, указывающий идентификатор
        //    заказа, который применяется для извлечения соответствующего объекта
        //    Order из хранилища, чтобы установить его свойство Shipped в true и сохранить.
        //Для отображения списка неотгруженных заказов добавим в папку Views/Order файл
        //представления Razor по имени List.Cshtml Элемент tаЫе используется для отображения
        //    ряда деталей, включая сведения о приобретенных товарах. == false

        [Authorize]
        public ViewResult List() => View(repository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);


            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View(new Order());
        //291Метод Checkout() возвращает стандартное представление и передает
        //новый объект ShippingDetails в качестве модели представления.Чтобы
        //создать представление создадим папку Views/Оrder и поместим в нее
        //файл представления Razor по имени Checkout.cshtml
        [HttpPost]
        //297Для завершения класса OrderController понадобится модифицировать 
        //конструктор т а к.чтобы он получал службы, требующиеся ему для обработки
        //    з а к а з а, и добавить новый метод действия.Который будет обрабатывать
        //    НТТР-запрос POST формы.Когда пользователь щелкает на кнопке Complete order (Завершить з а к а з ) .
        public IActionResult Checkout(Order order)
        {
            if(cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                //Корзина пуста!
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
        //Метод действи яCheckout() декорирован атрибутом HttpPost, т.е.он 
        //    будетвы­зываться для запроса POST -в этом случае, когда пользователь
        //    отправляет форму.Мы снова полагаемся на систему привязки моделей.
        //        Так что можно получить объект Order. Дополнить его данными из
        //        объекта Cart и сохранить в хранилище. Инфраструктура МVС контролирует 
        //        ограничения проверки достоверности, которые были применены к классу
        //        Order посредством атрибутов аннотаций данных, и через свойство
        //        ModelState сообщает методу действия о любых проблемах. Чтобы
        //        выяснить. Есть ли проблемы.Мы проверяем свойство ModelState. 
        //        IsValid.Мы вызы- аем метод ModelState.AddModelError () для 
        //        регистрации сообщения об ошибке. Если в корзине нет элементов.

        //300Для проверки пользовательских данных инфраструктура МVС будет
        //использовать атрибуты проверки достоверности.примененные к классу
        //    O rder.Тем не менее.Чтобы отобразить сообщения о проблемах,
        //    понадобится внести небольшое изменение.Здесь задействована
        //    еще одна встроенная вспомогательная функция дескриптора.
        //    Которая инспектирует состояние проверки достоверности данных.
        //    Предоставленных пользова­телем. И добавляет предупреждающие
        //    сообщения для каждой обнаруженной проблемы. Добавим в файлChe cko ut.cs h tm 
        //    элемента HTML, который будет обрабатываться этой вспомогательной функцией дескриптора.



        //Когда система ASP.NET Core Identity установлена и сконфигурирована, можно применить
        //    политику авторизации к тем частям приложения, которые необходимо защитить.Мы
        //    собираемся использовать самую базовую политику авторизации, которая предусматривает
        //    разрешение доступа любому пользователю, прошедшему аутентификацию.Хотя она может 
        //    оказаться полезной политикой также и в реальном приложении, существуют возможности
        //    для создания более детализированных элементов управления авторизацией, но из-за того,
        //    что в приложении SportsStore имеется только один пользователь, вполне достаточно
        //    провести различие между анонимными и аутентифицированными запросами.Атрибут Authorize
        //    применяется для ограничения доступа к методам действий, видно, что этот атрибут
        //    используется для защиты доступа к административным действиям в контроллере Order.

        //Мы не хотим препятствовать доступу пользователей, не прошедших аутентификацию, 
        //    к остальным методам действий в контроллере Order, так что атрибут Authorize
        //    применен только к методам List() и MarkShipped().
    }
}
