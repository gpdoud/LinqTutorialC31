using System;
using System.Linq;

namespace LinqTutorialC31 {
    class Program {
        static void Main(string[] args) {

            var customers = new Customer[] {
                new Customer { Id = 1, Name = "MAX" },
                new Customer { Id = 2, Name = "Jimmy Johns" }
            };
            var orders = new Order[] {
                new Order { Id = 1, Total = 100m, CustId = 2 },
                new Order { Id = 2, Total = 200m, CustId = 1 },
                new Order { Id = 3, Total = 300m, CustId = 2 }
            };
            var orderlines = new Orderline[] {
                new Orderline { Id = 1, OrderId = 1, Product = "Echo", Quantity = 1, Price = 100m },
                new Orderline { Id = 2, OrderId = 2, Product = "Echo", Quantity = 1, Price = 100m },
                new Orderline { Id = 3, OrderId = 2, Product = "EchoDot", Quantity = 2, Price = 50m },
                new Orderline { Id = 4, OrderId = 3, Product = "Echo", Quantity = 2, Price = 100m },
                new Orderline { Id = 5, OrderId = 3, Product = "EchoShow8", Quantity = 1, Price = 140m },
                new Orderline { Id = 6, OrderId = 3, Product = "Fire TV Stick", Quantity = 1, Price = 60m }
           };

            // Query Syntax: List all orders and the lines on those orders
            // display order: id, total; orderline: product, quantity, price

            #region Answer
            //var ordlines = from o in orders
            //               join l in orderlines
            //               on o.Id equals l.OrderId
            //               orderby o.Id
            //               select new {
            //                   o.Id, o.Total, l.Product, l.Quantity, l.Price,
            //                   Subtotal = l.Quantity * l.Price
            //               };
            #endregion





















            var custord = from c in customers
                          join o in orders
                          on c.Id equals o.CustId
                          join l in orderlines
                          on o.Id equals l.OrderId
                          orderby o.Total descending
                          group new { c, o, l } by new { o.Id, o.Total } into grp
                          select new {
                              Order = grp.Key.Id, OrderTotal = grp.Key.Total, 
                              CalcTotal = grp.Sum(x => x.l.Price * x.l.Quantity)
                          };
            foreach(var col in custord) {
                Console.WriteLine($"Order Nbr: {col.Order}, OrderTotal: {col.OrderTotal}, Total: {col.CalcTotal}");
            }

            var numbers = new int[] {
                8927, 2150, 2883, 2221, 3643, 4126, 5256, 9275, 7016, 1169,
                2681, 3087, 8256, 8125, 6865, 9366, 9547, 6634, 4739, 7914,
                9636, 8905, 9553, 4122, 8553, 9658, 8406, 8915, 7426, 7525,
                2279, 2724, 7744, 5838, 2630, 1483, 7161, 4514, 9937, 9453,
                3173, 5348, 3380, 4891, 5079, 8044, 5584, 6619, 8953, 4438,
                2543, 3843, 7468, 4139, 1426, 9309, 4631, 7133, 2527, 7507,
                2196, 2993, 3333, 9427, 3895, 3532, 8503, 4874, 2459, 5657,
                3086, 4653, 2396, 7749, 9524, 3291, 1895, 8809, 6948, 7992,
                3187, 4512, 1318, 6572, 9904, 2175, 6726, 9956, 3943, 3190,
                6469, 5237, 7988, 1240, 7585, 1458, 4339, 3120, 2976, 3659
            };

            var numbers2 = new int[] {
                3374,6512,6885,4146,4229,2752,3990,6406,1712,8844,
                9113,9427,5021,1455,7621,4933,2630,8245,2527,7931,
                9027,4463,7382,2411,7650,8503,1539,6115,7877,5338,
                1442,6126,2612,5965,7712,4034,3496,7151,3998,9566,
                3682,4607,6566,1426,7370,9807,9922,1355,7195,3687
            };

            // display numbers in numbers2 evenly divisible by 3
            var divsBy3 = from n in numbers2
                          where n % 3 == 0
                          select n;

                          #region Answer
            //var nbrsInBoth = from n1 in numbers
            //                 join n2 in numbers2
            //                 on n1 equals n2
            //                 select n1;

            //Console.WriteLine();
            //foreach(var n in nbrsInBoth) {
            //    Console.Write($"{n} ");
            //}
                          #endregion














            // Query Syntax: sum the numbers between 1500 and 3000 OR between 6500 and 8500
            // sorted in asc order
            var q3 = (from nbr in numbers
                      where (nbr > 1500 && nbr < 3000) || (nbr > 6500 && nbr < 8500)
                      orderby nbr
                      select nbr).Sum(n => n);
            #region Answer
            var q3a = numbers.Where(nbr => (nbr > 1500 && nbr < 3000) || (nbr > 6500 && nbr < 8500))
                                .OrderBy(n => n).ToList();
            #endregion
            // Query Syntax: number LT 2000 or GT 8000 sorted desc
            var q2 = from nbr in numbers
                     where nbr < 2000 || nbr > 8000
                     orderby nbr descending
                     select nbr;

            #region Answer
            var q2a = numbers.Where(nbr => nbr < 2000 || nbr > 8000)
                                .OrderByDescending(n => n).ToList();
            #endregion

            // Query syntax: number GTE 2500 and LTE 7500
            var q1 = from nbr in numbers
                     where nbr >= 2500 && nbr <= 7500
                     orderby nbr
                     select nbr;

            #region Answer
            var q1a = numbers.Where(nbr => nbr >= 2500 && nbr <= 7500)
                                .OrderBy(n => n).ToList();
            #endregion

            // Query syntax; number LT 5000 asc sequence
            var lessThan5000 = from nbr in numbers
                               where nbr < 5000
                               orderby nbr
                               select nbr;
            // Method syntax
            var lessThan5000A = numbers.Where(nbr => nbr < 5000).OrderBy(nbr => nbr).ToList();

        }
    }
}
