
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace CinemaDB.Models
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("jsconfig.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CinemaDbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (CinemaDbContext db = new CinemaDbContext(options))
            {
                var cinemas = db.Cinemas.ToList();
                var workers = db.Workers.ToList();
                var positions= db.Positions.ToList();
                Position booker = new Position { PName = "Бугалтер" };
               Worker worker4 = new Worker {CinemaId= cinemas.FirstOrDefault(c => c.CName == "Бескінечне літо").Id, WName= "Олена", WSurname="Тихонова",Passport= 88888,PositionId= booker.Id,Phone= "(079) 040-00-12",Salary= 30000 };
                //        var workers = db.Workers.Join(db.Positions, // второй набор
                //w => w.PositionId, // свойство-селектор объекта из первого набора
                //p => p.Id, // свойство-селектор объекта из второго набора
                //(w, p) => new // результат
                //{
                //    Name = w.WName,
                //    Surname = w.WSurname,
                //    Age = u.Age
                //});
                var lena = workers.FirstOrDefault(w => w.WName == "Олена" || w.WName == "Тоня");
                if (positions.FirstOrDefault(p => p.PName == "Бугалтер") == null)
                {
                    db.Positions.Add(booker);
                    db.SaveChanges();
                }
                if (lena == null)
                {
                    db.Workers.Add(worker4);
                    db.SaveChanges();

                }
                //else
                //{
                //    db.Workers.Remove(workers.FirstOrDefault(w => w.WName == "Олена"));
                //}
                db.SaveChanges();


                if (lena.WName == "Олена")
                {
                    lena.WName = "Тоня";
                    db.Workers.Update(lena);
                    db.SaveChanges();
                }
                workers = db.Workers.ToList();

                foreach (Cinema cinema in cinemas)
                {
                    Console.WriteLine("Кінотеатр:");
                    Console.WriteLine($"{cinema.Id}.{cinema.CName}, {cinema.CAddress}, {cinema.AdminPhone}");
                    Console.WriteLine("Працівники:");
                    foreach (Worker worker in workers)
                    {
                        foreach (Position position in positions)
                        {
                            if (cinema.Id == worker.CinemaId && worker.PositionId==position.Id)
                                Console.WriteLine($"{worker.Id}.{worker.WName} {worker.WSurname}, Зарплатня: {worker.Salary}, Посада:{position.PName}");
                        }
                    }
                }
            }
        }
    }
}
