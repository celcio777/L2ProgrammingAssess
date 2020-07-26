using System;
using System.Collections.Generic;
using System.Text;

namespace L2DigiProgrammingAssesment
{
    class V1
    {
        static string name1, name2;
        static float price1, price2, size1, size2;
        public static void V1Class()
        {
            Console.WriteLine("What are the Name, Price, and size of your products?");
            Console.WriteLine("1st Name:");
            name1 = Console.ReadLine();
            Console.WriteLine("1st Price:");
            price1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter all sizes in the same units");
            Console.WriteLine("1st Size:");
            size1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Next Product:");
            Console.WriteLine("2nd Name:");
            Console.WriteLine("2nd Price:"); 
            Console.WriteLine("2nd Size:");

        }
    }
}
