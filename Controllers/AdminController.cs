using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repositoty;
        public AdminController(IProductRepository repo)
        {
            repositoty = repo;
        }
        public ViewResult Index() => View(repositoty.Products);
        //Метод действия Edit( ) , добавлен в контроллер Admin. Будет получать 
        //НТГР-запрос, отправляемый браузером, когда пользователь щелкает на кнопке Edit.
        //Этот простой метод ищет товар с идентификатором, соответствующим значению параметра 
        //productld, и передает его как объект модели представления методу View().

        public ViewResult Edit(int productId) =>
            View(repositoty.Products.FirstOrDefault(p => p.ProductID == productId));
        //Располагая методом действия, можно создать представление для отображения. 
        //Добавим в папку Views/Admin файл представления Razor по имени Edi t. cshtml

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repositoty.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return 
                 RedirectToAction("Index");
               
                
            }
            else
            {
                //Что-то не так со значениями данных
                return View(product);
            }
        }
        public ViewResult Create() => View("Edit", new Product());
        [HttpPost]
        public IActionResult Delete(int productID)
        {
            Product deletedProduct = repositoty.DeleteProduct(productID);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted"; 
            }
            return RedirectToAction("Index"); 
        }
        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
    //Начнем с создания отдельного контроллера для управления каталогом
    //товаров.Добавим в папку Controllers файл класса по имени AdminController.cs
    //В конструкторе контроллера объявлена зависимость от интерфейса IProductRepository, 
    //которая будет распознаваться при создании экземпляров.В классе контроллера определен
    //    единственный метод действия Index() , который вызывает метод View(), чтобы выбрать
    //    стандартное представление для действия, и передает ему в качестве модели представления 
    //    набор товаров из базы данных.

    //Нам нужно защитить все методы действий, определяемые контроллером Admin, 
    //    чего можно достичь за счет применения атрибута Authorize к самому классу
    //    контроллера, что приведет к применению политики авторизации ко всем содержащимся
    //    в нем методам действий
}
