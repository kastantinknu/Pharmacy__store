using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;


namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        //данные подлежащие передачи из контролера в представление - содержаться в единой модели представления
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        //Нам нужно обеспечить взаимодействие текущей категории с представлением, чтобы визуализировать боковую панель
        public string CurrentCategory { get; set; }
        //В класс ProductsListViewModel добавлено свойство по имени  CurrentCategory.
        //Следующий шаг заключается в обновлении класса ProductController,
        //чтобы метод действия List() фильтровал объекты Product по категории
        //и использовал  только что добавленное в модель представления свойство
        //для указания категории, выбранной в текущий момент.
    }
}
