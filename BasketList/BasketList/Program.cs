using System;
using System.Collections.Generic;
using BasketList.Collections;

namespace BasketList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var basket = new Basket<int>();

            for (var i = 0; i < 10; i++)
            {
                basket.Add(i);
            }

            DisplayBasket(basket);

            var nums = new int[] { 11, 22, 33, 1, 2 };

            basket.AddRange(nums);

            DisplayBasket(basket);

            basket.Remove(22);

            DisplayBasket(basket);

            basket.RemoveAt(3);

            DisplayBasket(basket);

            basket.Sort();

            DisplayBasket(basket);
        }

        public static void DisplayBasket<T>(Basket<T> basket)
        {
            foreach (var item in basket)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }
    }
}
