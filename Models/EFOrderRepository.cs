using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderID==0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
        //Класс EFOrderRepository реализует интерфейс IOrderRepository с
        //применением Entity Framework С оrе , позволяя извлекать набор 
            //сохраненных объектов Order и создавать либо изменять заказы.
    }
    //Реализация хранилища для заказов требует небольшой дополнительной работы. 
    //    И н ф р а с т р у к т у р у E п t i t y Framework Core 
    //    н е о б х о д и м о п р о и н с т р у к т и р о в а т ь о з а г р у з к е 
    //    связанных данных, если они охватывают несколько т а б л и ц . с помощью 
    //    методов Include () и Theninclude () у к а з а н о, ч т о к о г д а объект
    //    Order читается и з базы данных, то также должна загружаться коллекция,
    //    ассоциированная со свойством Lines, наряду с объектами Product, которые
    //    связаны с элементами к о л л е к ц и и :
    //    pubic IQueryaЫe<Order>Orders => context.Orders.Include(o => o.Lines).Theninclude(l => l.Product);
    //Такой подход гарантирует получение всех нужных объектов данных, невыполняя запросы и н е 
    //    собирая данные напрямую.Дополнительный шаг требуется и при сохранении объекта Order
    //    в базе данных.Когда данные корзины пользователя десериализируются и з состояния
    //    с е а н с а, п а к е т JSON создает новые о б ъ е к т ы, н е известные инфраструктуре
    //    E п t i t y Framework Core, которая затем п ы т а е т с я записать в с е объекты в
    //    базу данных.В случае объектов Product э т о о з н а ч а е т, ч т о инфраструктура
    //        E п t i t y Framework Core попытается записать о б ъ е к т ы , которые уже
    //        были с о хранены, ч т о приведет к ошибке. В о избежание проблемы мы
    //        уведомляем E п t i t y FrameworkCore о т о м , ч т о объекты существуют
    //        и н е должны сохраняться в базе данных до т е х п о р, п о к а они н е б у д у т
    //        модифицированы: context.AttachRange(order.Lines.Select(l => l.Product));
    //    В результате инфраструктура E п t i t y Framework Core не будет пытаться записывать
    //    десериализированные объекты Product, которые ассоциированы с объектом Order.
    //296Хранилище заказов регистрируется как служба внутри метода ConfigureServices () класса Startup.


    //297Для завершения класса OrderController понадобится модифицировать 
    //конструктор т а к.чтобы он получал службы, требующиеся ему для обработки
    //    з а к а з а, и добавить новый метод действия.Который будет обрабатывать
    //    НТТР-запрос POST формы.Когда пользователь щелкает на кнопке Complete order (Завершить з а к а з ) .
}
