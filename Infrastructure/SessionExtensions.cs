using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SportsStore.Infrastructure
{
    //Средство состояния сеансав ASP.NET Core хранит только значения int,
    //string и byte[]. Поскольку мы хотим сохранять объект Cart, необходимо
    //определить расширяющие методы для интерфейса ISession, которые предоставят
    //доступ к данным состояния сеанса с целью сериализаци и объектов Cart в формат
    //JSON и их обратного преобразования. Добавим в папку Infrastructure файл класса
    //по имени Session Extensions. cs


    //При сериализации объектов в формат JSON(JavaScript Object Notation -система
    //обозначений для объектов JavaScript) расширяющие методы полагаются на пакет
    //Json.NET.Пакет Json.NET не требуется добавлять к проекту, потому что он уже
    //используется " за кулисами" инфраструктурой МVС для поддержки средства заголовков JSON.

    //    Расширяющие методы облегчают сохранение и извлечение бъектов Cart.Для добавления
    // объекта Cart к состоянию сеанса в контроллере применяется следующий вызов: 
    //    HttpContext.Session.SetJson("Cart", cart);
    //Свойство HttpContext определено в базовом классе Controller, от которого обычно
    //унаследованы контроллеры, возвращает объект HttpContext.Этот объект предоставляет
    //данные контекста о запросе, который был получен.И ответе, находящемся в процессе
    //подготовки.СвойствоHttpContext.Session возвращает объект.Реализующий интерфейс
    //ISession.Данный интерфейс является именно тем типом, где мы определили метод SetJson(),
    //принимающий аргументы.В которых указываются ключ и объект, подлежащий добавлению в состояние сеанса.

    //Расширяющий метод сериализирует объект и добавляет его в состояние сеанса.И
    //спользуя функциональность, которая лежит в основе интерфейса ISession.
    //Для извлечения объекта Cart применяется другой расширяющий метод, которому
    //передается тот жесамый ключ Cart cart = HttpContext.Session.GetJson<Cart>("Cart");
    //Параметр типа позволяет указать тип объекта, который ожидается извлечь: этот тип
    //используется в процессе десериализации.
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
