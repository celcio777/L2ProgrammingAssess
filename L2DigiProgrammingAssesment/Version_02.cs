using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace L2DigiProgrammingAssesment
{
    class V2
    {
        static string name1, name2;
        static float price1, price2, size1, size2;
        public struct scoringClass
        {
            public int score1, score2;
        }
        public static void V2Main()
        {
            // gathering data (inefficient)
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
            // displaying the data back to user 
            Console.WriteLine("These are the products you have given:");
            Console.WriteLine($"1) {name1}: ${price1}, size: {size1}");
            Console.WriteLine($"2) {name2}: ${price2}, size: {size2}");
            scoringClass returnScores = compareRateProducts(price1, price2, size1, size2);
            hl();
            Console.WriteLine("These are the products ranked by score:");
            if(returnScores.score1 > returnScores.score2)
            {
                Console.WriteLine($"1) {name1}: ${price1}, size: {size1}, score: {returnScores.score1}");
                Console.WriteLine($"2) {name2}: ${price2}, size: {size2}, score: {returnScores.score2}");
            }else if(returnScores.score2 == returnScores.score1)
            {
                Console.WriteLine($"1) {name1}: ${price1}, size: {size1}, score: {returnScores.score1}");
                Console.WriteLine($"2) {name2}: ${price2}, size: {size2}, score: {returnScores.score2}");
            }
            else
            {
                Console.WriteLine($"2) {name2}: ${price2}, size: {size2}, score: {returnScores.score2}");
                Console.WriteLine($"1) {name1}: ${price1}, size: {size1}, score: {returnScores.score1}");
            }
        }
        // Convenient function for a quick Horizontal Line
        static void hl()
        {
            Console.WriteLine("########################");
            Console.WriteLine();
        }
        static scoringClass compareRateProducts(float tmpPrice1, float tmpPrice2, float tmpSize1, float tmpSize2)
        {
            scoringClass passScoring = new scoringClass();
            if(tmpPrice1 > tmpPrice2)
            {
                passScoring.score1++;
                passScoring.score2 += 2;
            }else if(tmpPrice2 > tmpPrice1)
            {
                passScoring.score1 += 2;
                passScoring.score2++;
            }
            if(tmpSize1 > tmpSize2)
            {
                passScoring.score1++;
                passScoring.score2 += 2;
            }else if(tmpSize2 > tmpSize1)
            {
                passScoring.score2++;
                passScoring.score1 += 2;
            }
            return passScoring;
        }
    }
}
