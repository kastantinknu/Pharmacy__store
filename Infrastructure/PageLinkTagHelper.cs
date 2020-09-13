using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{
    //вспомогательная функция дескриптора
    //класс вспомогательной функции дескриптора PageLinkTagHelper заполняет элемент div
    //элементами а, кторые соответствуют страницам товаров
    //вспомогательные функции дескрипторов удобный способ помещение логики с# в представление
    //легко тестируються
    //вспомогательная ф-я должна быть зарегистрирована.


    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper:TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }

        //Когда нужно генерировать более сложные URL, необходим способ получения
        //дополнительной информации от представления, не добавляя дополнительные
        //свойства к классу вспомогательной функции дескриптора. К счастью, классы
        //вспомогательных функций дескрипторов обладают удобным  средством, которое
        //позволяет получать в одной коллекции все свойства с общим префиксом

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url")]
        //Декорирование свойства в классе вспомогательной функции дескриптора
        //посредством атрибута  HtmlAttributeName позволяет указывать префикс
        //для имен атрибутов элемента. Которым в  данном случае будет page-url.
        //Значение любого атрибута, чье имя начинается с такого префикса будет
        //добавлено в словарь, присваиваемый свойству PageUrlValues, которое
        //затем передается методу IUrlHelper.Action(), чтобы сгенерировать
        //URL для атрибута href элементов а , выпускаемых вспомогательной функцией дескриптора.
        public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();

    //Определяем в элементе div специальные атрибуты, которые указывают
    //требуемые классы и соответствуют свойствам, добавленным во вспомогательную
    //функцию дескриптора.Затем они применяются для стилизации создаваемых элементов <а>

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");


                PageUrlValues["productPage"] = i;

                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);


                //tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                //Значения атрибутов  автоматически используются для установки значений свойств
                //во вспомогательной функции дескриптора, учитывая отображение между форматом
                //имени атрибута HTML(page-class-normal) и форматом имени свойства С#(PageClassNormal).
                //Это позволяет вспомогательным функциям дескрипторов поразному реагировать в зависимости
                //от атрибутов НТМL-элемента, обеспечивая более гибкий способ генерации содержимого в
                //приложении МVС.
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                //преобразование в четабильные адреса
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
            //base.Process(context, output);
        }
    }
}
