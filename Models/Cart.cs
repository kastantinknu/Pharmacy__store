using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //268стр
    //Приложение успешно расширяется, но продавать какие-либо товары до тех пор,
    //пока не будет реализована корзина для покупок, не удастся.Кнопка добавления
    //в корзину (Add to cart) будет отображаться рядом с каждым товаром в каталоге.
    //Щелчок на ней будет приводить к выводу сводки по товарам.Которые уже были выбраны
    //пользователем, включая их общую стоимость. В этой точке пользователь может с помощью
    //кнопки продолжения покупки(Continue shopping) воз вратиться в каталог товаров, а
    //посредством кнопки перехода к оплате (Checkout now) сформировать заказ и завершить сеанс покупки.
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }

        }

        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;

        //Класс Cart использует класс CartLine.Который определен в том же
        //самом файле и представляет товар.Выбранный пользователем, а также
        //приобретаемое количество товара.Мы определили методы для добавления
        //элемента в корзину. Удаления элемента из корзины, вычисления общей
        //стоимости элементов в корзине и очистки корзины путем удаления всех
        //элементов.Мы также предоставили свойство, которое позволяет обратиться
        //к содержимому корзины с применением IEnurneraЫe<CartLine>.Все они легко
        //реализуются с помощью кода С# и небольшой доли кода LINQ.
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
