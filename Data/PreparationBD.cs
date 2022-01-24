using System;
using System.Collections.Generic;
using System.Linq;
using BankTask.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BankTask.Data
{
    // Класс для генерации тестовых данных, для тестирования API
    public static class PreparationBD
    {
        private const int CountData = 3;

        private static readonly string[] Names = {"Ivan", "Vasilii", "Dmitrii"};

        public static void PreparationPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                SeedFounderData(context);
                SeedClientData(context);
            }
        }

        private static void SeedFounderData(AppDbContext dbContext)
        {
            if (!dbContext.Founders.Any())
            {
                Console.WriteLine("Заполняем БД тестовыми данными для Founders!");

                var random = new Random();

                for (var i = 0; i < CountData; i++)
                {
                    var innString = "1101";
                    for (var j = 4; j < 10; j++)
                        innString += (char) random.Next('1', '9' + 1);

                    var start = new DateTime(2020, 1, 1);
                    var range = (DateTime.Today - start).Days;
                    var dateCreate = start.AddDays(random.Next(range));

                    dbContext.Founders.Add(new Founder()
                    {
                        INN = long.Parse(innString),
                        Name = Names[random.Next(Names.Length)],
                        DateCreate = dateCreate,
                        DateUpdate = DateTime.Now,
                    });
                }

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("В БД есть тестовые данные!");
            }
        }

        private static void SeedClientData(AppDbContext dbContext)
        {
            if (!dbContext.Founders.Any())
                throw new NullReferenceException("Заполните таблицу Founders, используя SeedFounderData!");

            if (!dbContext.Clients.Any())
            {
                Console.WriteLine("Заполняем БД тестовыми данными для Clients!");

                var random = new Random();

                for (var i = 0; i < CountData; i++)
                {
                    var name = "";
                    for (var j = 0; j < 10; j++)
                        name += (char) random.Next('A', 'Z' + 1);

                    var innString = "";
                    for (var j = 0; j < 10; j++)
                        innString += (char) random.Next('1', '9' + 1);

                    var start = new DateTime(2020, 1, 1);
                    var range = (DateTime.Today - start).Days;
                    var dateCreate = start.AddDays(random.Next(range));

                    var founders = new List<Founder>
                    {
                        dbContext.Founders.Find(0),
                    };

                    dbContext.Clients.Add(new Client()
                    {
                        INN = long.Parse(innString),
                        Name = name,
                        Type = (ClientType) random.Next(0, 2),
                        Founders = dbContext.Founders.ToList(),
                        DateCreate = dateCreate,
                        DateUpdate = DateTime.Now,
                    });
                }

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("В БД есть тестовые данные!");
            }
        }
    }
}