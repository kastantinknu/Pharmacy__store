using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        //Прежде чем можно будет обрабатывать результаты редактиоваия.хранилище
        //товаров понадобится расширить.добавив возможность сохранения изенений.
        //    Первым делом необходимо добавить к интерфейсу I P ro du ctR epos i t o ry новый ме­тод
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}
