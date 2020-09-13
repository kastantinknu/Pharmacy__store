﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter a product name")]
        //Введите наименование товара
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter a description")]
        //введите описание
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Please enter a positive price")]
        //введите положительное значение цены

        public decimal Price { get; set; }

        [Required(ErrorMessage ="Please specify a category")]
        //Укажите категорию

        public string Category { get; set; }

        
    }
}
