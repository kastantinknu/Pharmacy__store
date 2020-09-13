using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //Чтобы предоставить доступ к объектам Order, мы последуем томуже самому 
    //шаблону.Который использовался для хранилища товаров. Добавим в папку Models
    //    новый файл по имени IOrderRepository.cs
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
        //Для реализации интерфейса хранилища заказов добавим в папку Models
        //файл класса по имени EFOrderRepository.cs
    }
}
