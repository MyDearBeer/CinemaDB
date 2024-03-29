﻿
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading.Tasks;

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
            optionsBuilder.UseLazyLoadingProxies();//lazy loading
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            
                CinemaDbContext db = new CinemaDbContext(options);
                string[] workersName = { "Олег", "Дмитро", "Ліза", "Настя", "Герман", "Джеймс", "Андрій", "Еліна" };
                string[] workersSurName = { "Ставок", "Байрактар", "Залізо", "Українець", "Зеленський","Петрушка" };
            string[] positionsName = { "Букер", "Маркетолог", "Кіномеханік", "Касир" };
                var cinemas = db.Cinemas.ToList();
                var workers = db.Workers.ToList();
                var positions= db.Positions.ToList();
                Position booker = new Position { PName = "Бугалтер" };
               Worker workerExample1 = new Worker {CinemaId= cinemas.FirstOrDefault(c => c.CName == "Нескінченне літо").Id, WName= "Олена", WSurname="Тихонова",Passport= 88888,PositionId= booker.Id,Phone= "(079) 040-00-12",Salary= 30000 };


                var lena = db.Workers.AsNoTracking().FirstOrDefault(w => w.WName == "Олена" || w.WName == "Тоня");//дані що не відслідковуються
                if (positions.FirstOrDefault(p => p.PName == "Бугалтер") == null)
                {
                    db.Positions.Add(booker);
                    db.SaveChanges();
                }
                if (lena == null)
                {
                    db.Workers.Add(workerExample1);
                    db.SaveChanges();

                    }
            else
            {
                db.Workers.Remove(workers.FirstOrDefault(w => w.WName == "Олена"));
            }
            db.SaveChanges();


                //if (lena.WName == "Олена")
                //{
                //    lena.WName = "Тоня";
                //   // db.Workers.Update(lena);
                //    db.SaveChanges();
                //}
                workers = db.Workers.ToList();

                var cinemas1 = db.Cinemas.Where(p => EF.Functions.Like(p.CName, "%Бес%"));

                var workers1 = db.Workers.Include(u => u.Cinema).ToList(); //eagle loading

               //  db.Entry(db.Cinemas.FirstOrDefault(c=>c.CName== "Нескінченне літо")).Collection(c => c.WorkersNavigation).Load(); //explicit loading
                //workers = (from Worker in db.Workers
                //             where Worker.Position.PName == "Бармен"
                //             select Worker).OrderByDescending(s=>s.Salary).ToList();


                var workersPlusPositions = db.Workers.Join(db.Positions, 
      w => w.PositionId, 
      p => p.Id,
      (w, p) => new 
      {
          Id = w.Id,
          Name = w.WName,
          SurName = w.WSurname,
          Position = p.PName
      });
                var groups = from Worker in db.Workers
                             group Worker by Worker.Position.PName into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Count()
                             };

                var workersUnion = db.Workers.Where(w => w.Salary < 30000).Union(db.Workers.Where(w => EF.Functions.Like(w.Position.PName, "%Керуюч%")));
                var workersIntersert = db.Workers.Where(w => w.Salary >= 30000).Intersect(db.Workers.Where(w => EF.Functions.Like(w.Position.PName, "%Бар%")));
                var workersExcept = db.Workers.Where(w => w.Salary >= 30000).Except(db.Workers.Where(w => EF.Functions.Like(w.Position.PName, "%Бар%")));
                var workersDistinct = db.Workers.Select(w => w.Salary).Distinct();

                bool resultAny = db.Workers.Any(w => w.WName == "Аліса");
                bool resultAll = db.Workers.All(w => w.Cinema.CName == "Нескінченне літо");

                int minSalary = db.Workers.Min(w => w.Salary);
                int maxSalary = db.Workers.Max(w => w.Salary);
                double averageSalary = db.Workers.Average(w => w.Salary);
                int sumSalary = db.Workers.Sum(w => w.Salary);

                //var getMobNumByPos = db.GetMobNumByPos("Бармен");

                //SqlParameter param = new SqlParameter("@position", "Бармен");
                //var getMobNumByPos = db.Workers.FromSqlRaw("SELECT * FROM GetMobNumByPos (@position)", param).ToList();

                //var procedure = db.Workers.FromSqlRaw("ShowNum").ToList();

                object locker = new();

            

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
                Console.WriteLine();
                Console.WriteLine("Метод JOIN");

                foreach (var worker in workersPlusPositions)
                {
                    Console.WriteLine($"{worker.Id}.{worker.Name} {worker.SurName}, Посада:{worker.Position}");
                }

                Console.WriteLine();
                Console.WriteLine("Метод GROUP");

                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.Key} - {group.Count}");
                }

                Console.WriteLine();
                Console.WriteLine("Метод UNION");

                foreach (var worker in workersUnion)
                {
                    Console.WriteLine($"{worker.WName} {worker.WSurname}, Посада:{worker.Position.PName}");
                }

                Console.WriteLine();
                Console.WriteLine("Метод INTERSERT");

                foreach (var worker in workersIntersert)
                {
                    Console.WriteLine($"{worker.WName} {worker.WSurname}, Посада:{worker.Position.PName}");

                }

                Console.WriteLine();
                Console.WriteLine("Метод EXCEPT");

                foreach (var worker in workersExcept)
                {
                    Console.WriteLine($"{worker.WName} {worker.WSurname}, Посада:{worker.Position.PName}");
                }

                Console.WriteLine();
                Console.WriteLine("Метод DISTINCT");

                foreach (var worker in workersDistinct)
                {
                    Console.WriteLine($"Зарплатня: {worker}");
                }

                Console.WriteLine();
                Console.WriteLine("ANY та ALL");
                Console.WriteLine($"Чи усі працюють в кінотеатрі 'Нескінченне літо':{resultAll}");
                Console.WriteLine($"Чи є првцівник Аліса:{resultAny}");

                Console.WriteLine();
                Console.WriteLine("Інші агрегатні функції");
                Console.WriteLine($"Мінімальна зарплата:{minSalary}");
                Console.WriteLine($"Максимальна зарплата:{maxSalary}");
                Console.WriteLine($"Середня зарплата:{averageSalary}");
                Console.WriteLine($"Сумв:{sumSalary}");
            Console.WriteLine();

            //Console.WriteLine();
            //Console.WriteLine("Функція");
            //Console.WriteLine("Номера телефонів барменів:");
            //foreach (var worker in getMobNumByPos)
            //{
            //    Console.WriteLine($"{worker.WName}, {worker.Phone}");
            //}

            //Console.WriteLine("Процедурa");
            //Console.WriteLine("Прізвища, які починаються на Д:");

            //foreach (var worker in procedure)
            //{
            //    Console.WriteLine($"{worker.WSurname}, {worker.WName}");
            //}

            Semaphore sem = new Semaphore(1, 1);
            AutoResetEvent waitHandler = new AutoResetEvent(true);

            //for (int i = 1; i < 5; i++)
            //{

            //    Thread myThread1 = new(PrintWorker);
            //    Thread myThread2 = new(PrintPosition);
            //    myThread1.Start();
            //    myThread2.Start();
            //}

            
                Random rand = new Random();

           async void PrintPosition()
            {
                bool acquiredLock = false;
                //try
                //{
                //    Monitor.Enter(locker, ref acquiredLock);
                //lock (locker)
                //{
                sem.WaitOne();
                //waitHandler.WaitOne();
                Position position = new Position
                {
                    Id = new Random().Next(),
                    PName = positionsName[new Random().Next(0, positionsName.Length)],
                };

               
                while (await db.Positions.AnyAsync(p => p.PName == position.PName) == true|| await db.Positions.AnyAsync(p => p.Id == position.Id) == true)
                {
                    position.Id = new Random().Next();
                    position.PName = positionsName[new Random().Next(0, positionsName.Length)];
                    Console.WriteLine("Перевірка на існуючу посаду....");
                }


                Console.WriteLine("Додана посада");
                await db.Positions.AddAsync(position);
                await db.SaveChangesAsync();

                Console.WriteLine($"{Thread.CurrentThread.Name}:{position.PName}");

                Thread.Sleep(100);
                positions = await db.Positions.ToListAsync();
                sem.Release();
            };

                async void  PrintWorker()
                {

                    bool acquiredLock = false;
                ////try
                ////{
                ////    Monitor.Enter(locker, ref acquiredLock);
                ////lock (locker)
                ////{
                    sem.WaitOne();
               // //waitHandler.WaitOne();
                Worker work = new Worker
                    {
                        CinemaId = 2,
                        WName = workersName[new Random().Next(0, workersName.Length)],
                        WSurname = workersSurName[new Random().Next(0, workersSurName.Length)],
                        Passport = new Random().Next(10000, 99999),
                        PositionId = new Random().Next(0, 3),
                        Phone = $"(0{new Random().Next(10, 99)}) {new Random().Next(100, 999)}-{new Random().Next(10, 99)}-{new Random().Next(10, 99)}",
                        Salary = new Random().Next(5000, 70000)
                    };

                    // db.Workers.ToListAsync();
                    while (await db.Workers.AnyAsync(w => w.WSurname == work.WSurname) == true || await db.Workers.AnyAsync(w => w.WName == work.WName) == true)
                    {
                        work.WName = workersName[new Random().Next(0, workersName.Length)];
                        work.WSurname = workersSurName[new Random().Next(0, workersSurName.Length)];
                        Console.WriteLine("Перевірка на існуючого робітника....");
                    }
                

                    Console.WriteLine("Доданий робітник");
                   await db.Workers.AddAsync(work);
                    await db.SaveChangesAsync();

                    Console.WriteLine($"{Thread.CurrentThread.Name}:{work.WName} {work.WSurname}");
                    
                    Thread.Sleep(100);
                workers= await db.Workers.ToListAsync();
                sem.Release();
      
                //// waitHandler.Set();
                ////}
                ////finally
                ////{
                ////    if (acquiredLock)
                ////    {
                ////        Monitor.Exit(locker);

                ////    }
                ////}


                //// }
            }


            for (int i = 1; i < 5; i++)
            {

//                Task[] tasks = new Task[2]
//{
//      new Task(PrintWorker),
//              new Task(PrintPosition)
//            };

                //foreach (var t in tasks)
                //    t.Start();
                //Task.WaitAll(tasks);

                Task task1 = new Task(()=>
                {
                
                    PrintWorker();
                    
                }
            );
                Task task2 = new Task(() =>
                {
                 

                    PrintPosition();
                   
                });
                task1.Start();
                 task2.Start();
                task1.Wait();
                task2.Wait();
            }




            Console.WriteLine();
            }

           
        }
        }
    

