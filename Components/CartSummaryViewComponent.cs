using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{

    //Компонент представления CartSummaryViewComponent способен
    //задействовать в своих интересах службу, созданную ранее для
    //получения  объекта Cart, принимая ее как аргумент конструктора.
    //Результатом оказывается простой компонент представления, который
    //передает объект Cart методу View()
    //чтобы сгенерировать фрагмент НТМL-разметки для включения в компоновку.
    //Чтобы создать компоновку, создадим папку Views/Shared/Components/CartSummary,
    //добавим в нее файл представления Razor по имени Default.cshtml и поместим в него разметку,
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
