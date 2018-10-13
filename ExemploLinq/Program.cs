using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExemploLinq.Entities;

namespace ExemploLinq {
    class Program {

        static void Printer<T>(string message, IEnumerable<T> collection){

            Console.WriteLine(message);
            collection.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();

        }

        static void Main(string[] args) {

            try {

                Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
                Category c2 = new Category() { Id = 2, Name = "Computer", Tier = 1 };
                Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 1 };

                List<Product> products = new List<Product>(){
                  new Product() {Id = 1, Name = "Computer", Price = 1100.0, Category = c2},
                  new Product() {Id = 2, Name = "Hammer", Price = 90.0, Category = c1},
                  new Product() {Id = 3, Name = "Tv", Price = 1700.0, Category = c3},
                  new Product() {Id = 4, Name = "Notebook", Price = 1300.0, Category = c2},
                  new Product() {Id = 5, Name = "Saw", Price = 80.0, Category = c1},
                  new Product() {Id = 6, Name = "Tablet", Price = 700.0, Category = c2},
                  new Product() {Id = 7, Name = "Camera", Price = 700.0, Category = c3},
                  new Product() {Id = 8, Name = "Printer", Price = 350.0, Category = c3},
                  new Product() {Id = 9, Name = "MacBook", Price = 1800.0, Category = c2},
                  new Product() {Id = 10, Name = "Sound bar", Price = 700.0, Category = c3},
                  new Product() {Id = 11, Name = "Level", Price = 70.0, Category = c1},
                };

                var r1 = products.Where(x => x.Category.Tier == 1 && x.Price < 900);
                Printer("TIER 1 AND PRICE < 900:", r1);

                var r2 = products.Where(x => x.Category.Name == "Tools").Select(x => x.Name);
                Printer("NAME OF PRODUCTS FROM TOOLS:", r2);

                var r3 = products.Where(x => x.Name[0] == 'C')
                                 .Select(x => new { x.Name, x.Price, CategoryName = x.Category.Name });
                Printer("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT:", r3);

                var r4 = products.Where(x => x.Category.Tier == 1).OrderBy(x => x.Price).ThenBy(x => x.Name);
                Printer("TIER 1 ORDER BY PRICE THEN NAME:", r4);

                var r5 = r4.Skip(2).Take(4);
                Printer("TIER 1 ORDER BY PRICE THEN NAME SKIP 2 TAKE 4:", r5);

                var r6 = products.FirstOrDefault();
                Console.WriteLine("FIRST OR DEFAULT TEST 1: " + r6);

                var r7 = products.Where(x => x.Price > 3000.0).FirstOrDefault();
                Console.WriteLine("FIRST OR DEFAULT TEST 2: " + r7);
                Console.WriteLine();

                var r8 = products.Where(x => x.Id == 3).SingleOrDefault();
                Console.WriteLine("SINGLE OR DEFAULT TEST 1: " + r8);

                var r9 = products.Where(x => x.Id == 30).SingleOrDefault();
                Console.WriteLine("SINGLE OR DEFAULT TEST 2: " + r9);
                Console.WriteLine();

                var r10 = products.Max(x => x.Price);
                Console.WriteLine("MAX PRICE: " + r10);
                var r11 = products.Min(x => x.Price);
                Console.WriteLine("MIN PRICE: " + r11);
                Console.WriteLine();

                var r12 = products.Where(x => x.Category.Id == 1).Sum(x => x.Price);
                Console.WriteLine("CATEGORY 1 SUM PRICE: $:" + r12);
                var r13 = products.Where(x => x.Category.Id == 1).Average(x => x.Price);
                Console.WriteLine("CATEGORY 1 AVERAGE PRICE: $:" + r13);
                var r14 = products.Where(x => x.Category.Id == 5).Select(x => x.Price).DefaultIfEmpty(0.0).Average();
                Console.WriteLine("CATEGORY 5 AVERAGE PRICE: $:" + r14);
                Console.WriteLine();

                var r15 = products.Where(x => x.Category.Id == 1).Select(x => x.Price).Aggregate(0.0, (x, y) => x + y);
                Console.WriteLine("CATEGORY 1 AGGREGATE SUM: " + r15);
                Console.WriteLine();

                var r16 = products.GroupBy(x => x.Category);
                foreach (IGrouping<Category, Product> item in r16) {
                    Console.WriteLine("CATEGORY " + item.Key.Name + ":");
                    Printer("", item);
                }

            }
            catch (Exception) {

                throw;
            }
            finally{
                Console.ReadKey();
            }

        }
    }
}
