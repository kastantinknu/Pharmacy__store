using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    //272стр Необходимо модифицировать частичное представление
    //Views/Shared/ProductSummary.cshtml, добавив кнопки
    //к спискам товаров.В качестве подготовки добавим в
    //папку Infrastructure файл класса по имени UrlExtensions.cs
    //с определением расширяющего метода.
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
        //Расширяющий метод PathAndQuery() работает с классом HttpRequest,
        //используемый в ASP.NET Core для описания НТГР-запроса.Расширяющий
        //метод генерирует URL, по которому браузер будет возвращаться после
        //обновления корзины, принимая во внимание строку запроса, если она есть.
    }
}
