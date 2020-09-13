using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //Реализация метода SaveChanges()добавляет товар в хранилище, если
    //    значение ProductID равно О; в противном случае применяются
    //    изменения к существующей записи в базе данных.Мы не хотим
    //    здесь вдаваться в детали инфраструктуры Entity Framework
    //    Core Тем не менее, кое-какой код в методе SaveProduct()
    //    оказывает влияние на проектное решение.Положенное в основу
    //    приложения МVС.Нам известно.Что обновление должно выполняться.
    //        Когда получен параметр Product, который имеет не нулевое
    //        значение ProductID. Задача решается путем извлечения из
    //        хранилища объекта Product с тем же самым значением ProductID
    //        и обновления всех его свойств, чтобы они соответствовали
    //        значениям свойств объекта, переданного в качестве параметра.
    //        Причина таких действий в том, чтоинфраструктура Entity
    //        Framework Core отслеживает объекты. Которые она создает
    //        из базы данных. Объект.Переданный методу SaveChanges ().
    //        Создается системой привязки моделей MVC, т.е.инфраструктура
    //        Entity Framework Core ничего не знает о новом объекте Product,
    //        и она не будет применять обновление к базе данных, когда объект
    //        Product модифицирован. Существует множество способов решения
    //        указанной проблемы. Но мы принимаем самый простой из них.предполагающий
    //        поиск соответствующего объекта, о котором известно инфраструктуре
    //        Entity Framework Core, и его явное обновление.
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productID);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

    }
}
