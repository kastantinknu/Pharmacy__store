using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
    public class LoginModel
    {
        //Когда пользователь, непрошедший аутентификацию, посылает запрос,
        //который требует авторизации, он перенаправляется на URL вида /Account/Login,
        //    где приложение может предложить пользователю ввести свои учетные данные.
        //    В качестве подготовки создадим модель представления для учетных данных
        //    пользователя, добавив в папку Models/ViewModels файл класса по имени LoginModel.cs
        [Required]
        public string Name { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
