using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class PagingInfo
    {
        //модель представления хранит информацию о количестве доступных страниц, текущей странице,
        //и общем количестве товаров  в хранилище и передает их представлению
        //Модель представления служит для передачи даных между контролером и представлением.
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage);
    }
}
