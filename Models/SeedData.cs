using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public static class SeedData
    {
        //public static void EnsurePopulated(IApplicationBuilder app)
        //{
        //    ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
        //    context.Database.Migrate();
        //    //context.Dispose();
        //    //context.SaveChanges();
        //    //if (context.Products.Count<Product>() == 10)
        //    //{
        //    //    var rows = from o in context.Products
        //    //               select o;
        //    //    foreach (var row in rows)
        //    //    {
        //    //        context.Remove(row);
        //    //    }
        //    //    context.SaveChanges();
        //    //}
        //    if (!context.Products.Any())
        //    {
        //        //context.Products.RemoveRange(context.Products);

        //        context.Products.AddRange(
        //        //new Product
        //        //{
        //        //    Name = "Kayak",
        //        //    Description = "A boat for one person",
        //        //    Category = "Watersports",
        //        //    Price = 275
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Lifejacket",
        //        //    Description = "Protective and fashionable",
        //        //    Category = "Watersports",
        //        //    Price = 48.95m
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Soccer Ball",
        //        //    Description = "FIFA-approved size and weight",
        //        //    Category = "Soccer",
        //        //    Price = 19.50m
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Corner Flags",
        //        //    Description = "Give your playing field а professional touch",
        //        //    Category = "Soccer",
        //        //    Price = 34.95m
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Stadium",
        //        //    Description = "Flat-packed 35,000-seat stadium",
        //        //    Category = "Soccer",
        //        //    Price = 79500
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Thinking Сар",
        //        //    Description = "Improve brain efficiency bу 75%",
        //        //    Category = "Chess",
        //        //    Price = 16
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Unsteady Chair",
        //        //    Description = "Secretly give your opponent а disadvantage",
        //        //    Category = "Chess",
        //        //    Price = 29.95m
        //        //},
        //        //new Product
        //        //{
        //        //    Name = "Human Chess Board",
        //        //    Description = "А fun game for the family",
        //        //    Category = "Chess",
        //        //    Price = 75
        //        //},
        //        // new Product
        //        // {
        //        //     Name = "Bling-Bling King",
        //        //     Description = "Gold-plated, diamond-studded King",
        //        //     Category = "Chess",
        //        //     Price = 1200
        //        // });
        //                    new Product
        //                    {
        //                        Name = "Bioxtra Toothpaste 50ml",
        //                        Description = "BioXtra Toothpaste natural antimicrobial formulation helps to cleanse the mouth- tongue and teeth without burning or stinging.",
        //                        Category = "Oral Care",
        //                        Price = 275
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Daktarin Oral Gel 40g 20mg/g",
        //                        Description = "Daktarin Oral Gel 40g 20mg/g is for treating fungal and associated bacteria infections of the mouth or throat.",
        //                        Category = "Oral Care",
        //                        Price = 48.95m
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Opticlude Orthopic Eye Patch for occlusion therapy 5.7cm x 8.2cm",
        //                        Description = "Tried- proven and trusted through years of use by Orthoptists across Europe- 3M Opticlude™ Orthoptic Eye",
        //                        Category = "Eye Care",
        //                        Price = 19.50m
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Optrex Clear Eyes Eye Drops Solution 10ml",
        //                        Description = "Dusty smoky atmospheres, eye strain and chlorine in swimming pools can make the eyes red and bloodshot.",
        //                        Category = "Eye Care",
        //                        Price = 34.95m
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Face Mask 3ply bundle",
        //                        Description = "Face Mask 3ply is designed to resist moderate fluid splashes at 120mmHg while maintaining smooth breathability",
        //                        Category = "Covid-19",
        //                        Price = 79500
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Anti-Viral Sanitising Spray",
        //                        Description = "Medicare Effigerm First Aid Spray",
        //                        Category = "Covid-19",
        //                        Price = 16
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Gloves",
        //                        Description = "Mediskin Latex Gloves Powderfree",
        //                        Category = "Covid-19",
        //                        Price = 29.95m
        //                    },
        //                    new Product
        //                    {
        //                        Name = "Sambucol Black",
        //                        Description = "Sambucol Black Elderberry Liquid 120ml",
        //                        Category = "Vitamins",
        //                        Price = 75
        //                    },
        //                     new Product
        //                     {
        //                         Name = "SONA ZinC Chewable 30’s",
        //                         Description = "Sona ZinC chewable is a strawberry and vanilla flavoured chewable tablet containing immune boosting ingredients such as zinc and vitamin C",
        //                         Category = "Vitamins",
        //                         Price = 1200
        //                     });
        //        context.SaveChanges();
        //    }

        //}
        public static void EnsurePopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Bioxtra Toothpaste 50ml",
                        Description = "BioXtra Toothpaste natural antimicrobial formulation helps to cleanse the mouth- tongue and teeth without burning or stinging.",
                        Category = "Oral Care",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Daktarin Oral Gel 40g 20mg/g",
                        Description = "Daktarin Oral Gel 40g 20mg/g is for treating fungal and associated bacteria infections of the mouth or throat.",
                        Category = "Oral Care",
                        Price = 48.95m
                    },
                    new Product
                    {
                        Name = "Opticlude Orthopic Eye Patch for occlusion therapy 5.7cm x 8.2cm",
                        Description = "Tried- proven and trusted through years of use by Orthoptists across Europe- 3M Opticlude™ Orthoptic Eye",
                        Category = "Eye Care",
                        Price = 19.50m
                    },
                    new Product
                    {
                        Name = "Optrex Clear Eyes Eye Drops Solution 10ml",
                        Description = "Dusty smoky atmospheres, eye strain and chlorine in swimming pools can make the eyes red and bloodshot.",
                        Category = "Eye Care",
                        Price = 34.95m
                    },
                    new Product
                    {
                        Name = "Face Mask 3ply bundle",
                        Description = "Face Mask 3ply is designed to resist moderate fluid splashes at 120mmHg while maintaining smooth breathability",
                        Category = "Covid-19",
                        Price = 79500
                    },
                    new Product
                    {
                        Name = "Anti-Viral Sanitising Spray",
                        Description = "Medicare Effigerm First Aid Spray",
                        Category = "Covid-19",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Gloves",
                        Description = "Mediskin Latex Gloves Powderfree",
                        Category = "Covid-19",
                        Price = 29.95m
                    },
                    new Product
                    {
                        Name = "Sambucol Black",
                        Description = "Sambucol Black Elderberry Liquid 120ml",
                        Category = "Vitamins",
                        Price = 75
                    },
                     new Product
                     {
                         Name = "SONA ZinC Chewable 30’s",
                         Description = "Sona ZinC chewable is a strawberry and vanilla flavoured chewable tablet containing immune boosting ingredients such as zinc and vitamin C",
                         Category = "Vitamins",
                         Price = 1200
                     });
                context.SaveChanges();
            }
        }
    }
}
