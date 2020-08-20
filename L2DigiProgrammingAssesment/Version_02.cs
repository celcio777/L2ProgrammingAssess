using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;

namespace L2DigiProgrammingAssesment
{
    class V2
    {
        static string name1, name2;
        static float price1, price2, size1, size2;
        public class scoring{
            public int score1 = 0, score2 = 0;
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
            scoring scoringDisplay = calculateScores(price1,price2,size1,size2);
            // displaying the data back to user 
            Console.WriteLine("These are the products you have given:");
            Console.WriteLine($"1) {name1}: ${price1}, size: {size1}");
            Console.WriteLine($"2) {name2}: ${price2}, size: {size2}");
            hl();
            // displaying ranked data
            Console.WriteLine("And here is the ranked order from logical best to worst:");
            if(scoringDisplay.score1 > scoringDisplay.score2){
                Console.WriteLine($"1) {name1}: ${price1}, size: {size1}, Overall Score: {scoringDisplay.score1}");
                Console.WriteLine($"2) {name2}: ${price2}, size: {size2}, Overall Score: {scoringDisplay.score2}");
            }else if(scoringDisplay.score1 < scoringDisplay.score2 ){
                Console.WriteLine($"1) {name2}: ${price2}, size: {size2}, Cverall Score: {scoringDisplay.score2}");
                Console.WriteLine($"2) {name1}: ${price1}, size: {size1}, Overall Score: {scoringDisplay.score1}");
            }
            else
            {
                Console.WriteLine($"1) {name1}: ${price1}, size: {size1}, Overall Score: {scoringDisplay.score1}");
                Console.WriteLine($"2) {name2}: ${price2}, size: {size2}, Overall Score: {scoringDisplay.score2}");
            }
        }
        // method to calculate "scoreboard" of products
        static private scoring calculateScores(float tmpPrice1, float tmpPrice2, float tmpSize1, float tmpSize2)
        {
            
            // creating copy of the scoring struct to pass through function
            scoring scoringCopy = new scoring();
            if(tmpPrice2 > tmpPrice1){
                scoringCopy.score1+=2;
                scoringCopy.score2++;
            }else if(tmpPrice1 > tmpPrice2){
                scoringCopy.score1++;
                scoringCopy.score2+=2;
            }
            if(tmpSize1 > tmpSize2) {
                scoringCopy.score1 +=2;
                scoringCopy.score2++;
            }else if(tmpSize1<tmpSize2){
                scoringCopy.score1++;
                scoringCopy.score2+=2;
            }
            return(scoringCopy);
        }

        // Convenient function for Horizontal Line (hl)
        static void hl()
        {
            Console.WriteLine("########################");
            Console.WriteLine();
        }
    }
}
