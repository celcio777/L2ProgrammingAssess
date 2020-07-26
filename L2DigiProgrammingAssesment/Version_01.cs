using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace L2DigiProgrammingAssesment
{
    class V1
    {
        static string name1, name2;
        static float price1, price2, size1, size2;
        public static void V1Main()
        {
            Console.WriteLine("What are the Name, Price, and size of your products?");
            Console.WriteLine("1st Name:");
            name1 = Console.ReadLine();
            Console.WriteLine("1st Price:");
            price1 = float.Parse(Console.ReadLine());
            Console.WriteLine("Please enter all sizes in the same units");
            Console.WriteLine("1st Size:");
            size1 = float.Parse(Console.ReadLine());
            Console.WriteLine("Next Product:");
            Console.WriteLine("2nd Name:");
            name2 = Console.ReadLine();
            Console.WriteLine("2nd Price:");
            price2 = float.Parse(Console.ReadLine());
            Console.WriteLine("2nd Size:");
            size2 = float.Parse(Console.ReadLine());
            hl();
            Console.WriteLine("These are the products you have given:");
            Console.WriteLine($"1) {price1}: ${price1}, size: {size1}");
            Console.WriteLine($"2) {price2}: ${price2}, size: {size2}");
        }
        static void hl()
        {
            Console.WriteLine("########################");
            Console.WriteLine();
        }
    }
}
