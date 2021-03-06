﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using SampleSupport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private readonly DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Task 1")]
        [Description("Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)")]
        public void Linq001()
        {
            int[] arr = { 100, 1000, 10000, 100000 };

            foreach (var x in arr)
            {
                LinqSamples.WriteHeader($"x is {x}");

                var products = this.dataSource.Customers
                    .Where(cust => cust.Orders.Sum(order => order.Total) > x)
                    .Select(cust => $"Name = {cust.CompanyName}, Order's total price = {cust.Orders.Sum(order => order.Total)}");

                LinqSamples.Show(products);
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 2")]
        [Description("Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе. Сделайте задания с использованием группировки и без")]
        public void Linq002()
        {
            var сustomers = this.dataSource.Customers;
            var suppliers = this.dataSource.Suppliers;

            var result =
                from supplier in suppliers
                from customer in сustomers
                where supplier.Country == customer.Country &&
                      supplier.City == customer.City
                select $"Company {customer.CompanyName} and supplier {supplier.SupplierName} are in the same city {supplier.City} and country {supplier.Country}";

            LinqSamples.WriteHeader("Ingroupped");

            LinqSamples.Show(result);

            var grouppedResult =
                from grouppedCollection in (
                    from supplier in suppliers
                    from customer in сustomers
                    where supplier.Country == customer.Country &&
                          supplier.City == customer.City
                    select new
                    {
                        Customer = customer,
                        Supplier = supplier,
                    })
                group grouppedCollection by grouppedCollection.Customer.Country;

            LinqSamples.WriteHeader("Groupped");

            if (grouppedResult.Any())
            {
                foreach (var p in grouppedResult)
                {
                    ObjectDumper.Write($"In {p.Key} there's");

                    foreach (var item in p)
                    {
                        ObjectDumper.Write($"    company {item.Customer.CompanyName} and supplier {item.Supplier.SupplierName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No such objects");
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 3")]
        [Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]
        public void Linq003()
        {
            int[] arr = { 100, 1000, 10000, 100000 };

            foreach (var x in arr)
            {
                ObjectDumper.Write("");
                ObjectDumper.Write("");
                ObjectDumper.Write($"x is {x}");
                ObjectDumper.Write("");
                ObjectDumper.Write("");

                var result = this.dataSource.Customers
                    .Where(cust => cust.Orders.Any(order => order.Total > x))
                    .Select(cust => $"Customer {cust.CompanyName} have order with {cust.Orders.First(order => order.Total > x).Total} total");

                LinqSamples.Show(result);
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 4")]
        [Description("Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)")]
        public void Linq004()
        {
            var result = this.dataSource.Customers
                .Select(cust => $"{cust.CompanyName} is client since {(cust.Orders.Any() ? cust.Orders.Min(order => order.OrderDate).ToString("MMMM yyyy") : "never")}");

            LinqSamples.Show(result);
        }

        [Category("Restriction Operators")]
        [Title("Task 5")]
        [Description("Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента")]
        public void Linq005()
        {
            var result = this.dataSource.Customers
                .Where(cust => cust.Orders.Any())
                .ToDictionary(cust => cust,
                    cust => cust.Orders.OrderBy(order => order.OrderDate).FirstOrDefault())
                .OrderBy(kvp => kvp.Value.OrderDate.Year)
                .ThenBy(kvp => kvp.Value.OrderDate.Month)
                .ThenByDescending(kvp => kvp.Key.Orders.Sum(order => order.Total))
                .ThenBy(kvp => kvp.Key.CompanyName)
                .Select(dict => $"{dict.Key.CompanyName} is client since {dict.Value.OrderDate.ToString("yyyy MMMM")}, total is {dict.Key.Orders.Sum(order => order.Total)}");

            LinqSamples.Show(result);
        }

        [Category("Restriction Operators")]
        [Title("Task 6")]
        [Description("Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).")]
        public void Linq006()
        {
            var result = this.dataSource.Customers
                .Where(cust => string.IsNullOrWhiteSpace(cust.PostalCode) ||
                    cust.PostalCode.Any(c => !char.IsDigit(c)) ||
                    string.IsNullOrWhiteSpace(cust.Region) ||
                    !cust.Phone.StartsWith("(", StringComparison.InvariantCultureIgnoreCase))
                .Select(cust => $"Customer {cust.CompanyName} have postal code {cust.PostalCode}, region {cust.Region}, phone {cust.Phone}");

            LinqSamples.Show(result);
        }

        [Category("Restriction Operators")]
        [Title("Task 7")]
        [Description("Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости")]
        public void Linq007()
        {
            var result =
                from product in this.dataSource.Products
                group product by product.Category
                into groupedByCategoryProducts
                select
                    new KeyValuePair<string, IEnumerable<KeyValuePair<decimal, IEnumerable<Product>>>>(
                        groupedByCategoryProducts.Key,
                        from p in groupedByCategoryProducts
                        group p by p.UnitsInStock
                        into asd
                        select new KeyValuePair<decimal, IEnumerable<Product>>(asd.Key, asd.OrderBy(a => a.UnitPrice)));

            LinqSamples.Show(result);
        }

        [Category("Restriction Operators")]
        [Title("Task 8")]
        [Description("Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами")]
        public void Linq008()
        {
            const int min = 10;
            const int max = 100;

            var dict = new Dictionary<string, uint>
            {
                { "Low", 0},
                { "Middle", 1},
                { "High", 2},
            };

            var result = this.dataSource.Products
                .GroupBy(product => LinqSamples.GetRange(product, min, max),
                    product => $"Name = {product.ProductName}, price is {product.UnitPrice.ToString(".##")}")
                .OrderBy(kvp => dict[kvp.Key]);

            LinqSamples.Show(result);
        }

        [Category("Restriction Operators")]
        [Title("Task 9")]
        [Description("Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)")]
        public void Linq009()
        {
            var profitability = from customer in this.dataSource.Customers
                group customer by customer.City
                into groupped
                select
                    new KeyValuePair<string, decimal>(groupped.Key,
                        groupped.SelectMany(c => c.Orders).Average(o => o.Total));

            LinqSamples.WriteHeader("Profitability");
            LinqSamples.Show(profitability);

            var intensity = from customer in this.dataSource.Customers
                group customer by customer.City
                into groupped
                select
                    new KeyValuePair<string, double>(groupped.Key, groupped.Average(customer => customer.Orders.Length));

            LinqSamples.WriteHeader("Intensity");
            LinqSamples.Show(intensity);
        }

        [Category("Restriction Operators")]
        [Title("Task 10")]
        [Description("Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года), статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).")]
        public void Linq010()
        {
            var clients = this.dataSource.Customers;

            LinqSamples.WriteHeader("Mounthly");

            var mounthly =
                from order in clients.SelectMany(client => client.Orders).Select(o => o.OrderDate)
                group order by order.Month
                into groupped
                select
                    new KeyValuePair<string, int>(
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupped.Key),
                        groupped.Count());

            this.Show(mounthly);

            LinqSamples.WriteHeader("Yearly");

            var yearly = from order in clients.SelectMany(client => client.Orders).Select(o => o.OrderDate)
                group order by order.Year
                into groupped
                select
                    new KeyValuePair<int, int>(groupped.Key,
                        groupped.Count());

            LinqSamples.Show(yearly.OrderBy(g => g.Key));

            LinqSamples.WriteHeader("Bothly");

            var bothly = from order in clients.SelectMany(client => client.Orders).Select(o => o.OrderDate)
                group order by new {year = order.Year, mounth = order.Month}
                into groupped
                select
                    new
                    {
                        Key = new KeyValuePair<int, int>(groupped.Key.year, groupped.Key.mounth),
                        Value = groupped.Count(),
                    };

            foreach (var item in bothly.OrderBy(g => g.Key.Key).ThenBy(g => g.Key.Value))
            {
                ObjectDumper.Write($"{item.Key.Key}, {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Key.Value)} - {item.Value}");
            }
        }

        private static string GetRange(Product product, int a, int b)
        {
            return product.UnitPrice < a
                ? "Low"
                : product.UnitPrice < b
                    ? "Middle"
                    : "High";
        }

        private static void Show(IEnumerable<KeyValuePair<string, decimal>> collection)
        {
            foreach (var iten in collection)
            {
                ObjectDumper.Write($"{iten.Key} - {iten.Value.ToString(".##")}");
            }
        }

        private static void Show(IEnumerable<KeyValuePair<string, double>> collection)
        {
            foreach (var iten in collection)
            {
                ObjectDumper.Write($"{iten.Key} - {iten.Value.ToString(".##")}");
            }
        }

        private static void Show(IEnumerable<KeyValuePair<int, int>> yearly)
        {
            foreach (var item in yearly)
            {
                ObjectDumper.Write($"{item.Key} - {item.Value}");
            }
        }

        private static void Show(IEnumerable<string> products)
        {
            if (products.Any())
            {
                foreach (var p in products)
                {
                    ObjectDumper.Write(p);
                }
            }
            else
            {
                Console.WriteLine("No such objects");
            }
        }

        private static void Show(IEnumerable<IGrouping<string, string>> products)
        {
            if (products.Any())
            {
                foreach (var p in products)
                {
                    ObjectDumper.Write(p.Key);

                    foreach (var item in p)
                    {
                        ObjectDumper.Write("    " + item);
                    }
                }
            }
            else
            {
                Console.WriteLine("No such objects");
            }
        }

        private static void Show(IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<decimal, IEnumerable<Product>>>>> products)
        {
            if (products.Any())
            {
                foreach (var p in products)
                {
                    ObjectDumper.Write(p.Key);

                    foreach (var pair in p.Value)
                    {
                        ObjectDumper.Write("    " + pair.Key);

                        foreach (Product product in pair.Value)
                        {
                            ObjectDumper.Write($"        {product.ProductName}: {product.UnitPrice.ToString(".##")}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No such objects");
            }
        }

        private static void WriteHeader(string v)
        {
            ObjectDumper.Write("");
            ObjectDumper.Write("");
            ObjectDumper.Write(v);
            ObjectDumper.Write("");
            ObjectDumper.Write("");
        }

        private void Show(IEnumerable<KeyValuePair<string, int>> mounthly)
        {
            foreach (var item in mounthly)
            {
                ObjectDumper.Write($"{item.Key} - {item.Value}");
            }
        }
    }
}