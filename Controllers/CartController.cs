using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using Microsoft.AspNetCore.Http.Extensions;

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    //Для обработки щелчков на кнопках AddТоCart понадобится создать контроллер.
    //Добавим в  папку Controllers файл класса по имени CartController.cs

    //Для сохранения и извлечения объектов Cart применяется средство состояния
    //сеанса ASP.NET, с которыми взаимодействует метод GetCart() . Промежуточное
    //программное обеспечение, зарегистрированное в предыдущем разделе, использует
    //сооkiе-наборы или переписывание URL, чтобы ассоциировать вместе множество
    //запросов от определенного пользователя с целью формирования отдельного сеанса
    //просмотра. Связанным средством является состояние сеанса, которое ассоциирует
    //данные с сеансом. Ситуация идеально подходит для класса Cart: мы хотим, чтобы
    //каждый пользователь имел собственную корзину, и она сохранялась между запросами.
    //Данные, связанные с сеансом, удаляются по истечении времени существования сеанса
    //(обычно из-за того, что пользователь не отправляет запрос какое-то время,
    //т.е.управлять хранилищем или жизненным циклом объектов Cart не придется.
    //В методах действий AddToCart () и RemoveFromCart () применялись имена параметров,
    //которые соответствуют именам элементов input в НТМL-формах, созданных в представлении
    //ProductSummary. cshtml.Это позволяет инфраструктуре MVC ассоциировать входящие
    //переменные НТГР-запроса POST формы с параметрами и означает, что делать что-то
    //самостоятельно для обработки формы не нужно.Такой процесс называется привязкой
    //модели и с его помощью можно значительно упрощать классы контроллеров

    public class CartController : Controller
    {
        private IProductRepository repositoty;

        //services
        private Cart cart;

        //public CartController(IProductRepository repo)
        //{
        //    repositoty = repo;
        //}

        //services
        public CartController(IProductRepository repo, Cart cartService)
        {
            repositoty = repo;
            cart = cartService;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repositoty.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //Cart cart = GetCart();
                //SaveCart(cart);

                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productID, string returnUrl)
        {
            Product product = repositoty.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                //Cart cart = GetCart();
                //SaveCart(cart);

                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new {returnUrl});
        }


        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}

        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetObjectAsJson("Cart", cart);
        //}

        //public IActionResult Index()
        //    => Json(new { message = "Test" });
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            }); ;
        }
        
    }
}
