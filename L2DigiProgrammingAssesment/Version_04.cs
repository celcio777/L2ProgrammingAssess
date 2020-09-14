﻿using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace L2DigiProgrammingAssesment
{
    class V4
    {
        static string[] name, nameRetain;
        static float[] price, size, priceRetain, sizeRetain;
        public static int[] score;
        public enum measurementType
        {
            WEIGHT,
            VOLUME
        }
        public static void V4Main()
        {
            bool rept = true; // loop for another product
            int totI = 0; // the current product to be stored, The unit type (mas/vol)
            // gathering data (inefficient)
            hl();
            Console.WriteLine("How many products will you be comparing?");
            inputValid(totI, "i");
            Console.WriteLine("What are the Name, Price, and size of your products?");
            Console.WriteLine("(You can enter \"done\" at the end of entering a product to finish the process)");
            while (rept)
            {
                Console.WriteLine($"Name {totI + 1}:");
                string tmpNam = Console.ReadLine();
                if (tmpNam != "")
                {
                    name[totI] = tmpNam;
                }
                else
                {
                    name[totI] = $"Product {totI + 1}";
                }

                Console.WriteLine($" Price {totI + 1}:");
                inputValid(totI, "p");
                Console.WriteLine("Please enter all sizes in the same unit type e.g: kg & g, L & mL");
                Console.WriteLine($"Size {totI + 1}:");
                inputValid(totI, "s");
                totI++;
                Console.WriteLine();
                if (totI >= name.Length - 4 && name.Length - totI > 0)
                {
                    Console.WriteLine("You have entered the amount of products that you indicated.");
                    Console.WriteLine($"Do you need to add more? You have {name.Length - totI} more available");
                    while (true)
                    {
                        Console.Write("[y/n]");
                        string cont = Console.ReadLine();
                        if (cont == "n")
                        {
                            rept = false;
                            break;
                        }
                        else if (cont == "y")
                        {
                            break;
                        }
                    }

                }
                else if (name.Length - totI > 0)
                {
                    Console.WriteLine("<Enter> to continue or \"done\" to finish");
                    string completed = Console.ReadLine();
                    if (completed != "")
                    {
                        Console.WriteLine("Are you sure you would like to finish? [y/n]");
                        if (Console.ReadLine() == "y")
                        {
                            break;
                        }
                    }
                }
                else
                {
                    rept = false;
                    break;
                }
            }

            hl();
            // displaying the data back to user 
            Console.WriteLine("These are the products you have given:");
            Console.WriteLine();
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != null)
                {
                    Console.WriteLine($"{i + 1}) {name[i]}> Price: ${price[i]}, Size: {size[i]}");
                    Thread.Sleep(100);// Short delay whilst displaying for a smooth interface
                }
            }
            hl();
            Console.WriteLine("Here is a list of your products ranked by size and price:");
            int[] returnScore = compareRateProducts(price, size); // array to store the returned score values in the long-term
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != null)
                {
                    int indexTopScore = Array.IndexOf(score, score.Max());
                    Console.WriteLine($"{i + 1}) {nameRetain[indexTopScore]}> Price: ${priceRetain[indexTopScore]}, size: {sizeRetain[indexTopScore]}, Score: {returnScore[indexTopScore]}pts.");
                    score[indexTopScore] = -2; // ensuring that the same product's score won't be called twice by setting it negative.
                }
            }
        }
        // input error checking
        static void inputValid(int iteration, string forVal)
        {
            string tmpVal, keepVal;
            while (true)
            {
                tmpVal = Console.ReadLine();
                keepVal = tmpVal;
                if (float.TryParse(tmpVal, out float parsed))
                {
                    valueInput(parsed, tmpVal);
                    Console.WriteLine("Value submitted");
                    break;
                }
                else
                {
                    tmpVal = Regex.Replace(tmpVal, "[^0-9.]", "");
                    if (float.TryParse(tmpVal, out float parsed2))
                    {
                        Console.WriteLine($"Sorry, I didn't understand that, did you mean: \"{parsed2}\"? [y/n]");
                        string yn = new string(Console.ReadLine());
                        if (yn == "y")
                        {
                            valueInput(parsed, keepVal);
                            Console.WriteLine("Value submitted");
                            break;
                        }
                        else if (yn == "n")
                        {
                            Console.WriteLine("Okay, please try again.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured, please try again:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry, I couldn't read that as a number value, please enter a number input.");
                    }
                }
            }
            void valueInput(float valueToEnter, string recievedString)
            {
                string[] symbolsMass = new string[] { "kg", "g" };
                int[] unitsMass = new int[] { 1000, 1 };

                string[] symbolsVol = new string[] { "L", "mL" };
                int[] unitsVol = new int[] { 1000, 1 };
                int multiplier = 1;

                if (forVal == "s")
                {
                    size[iteration] = valueToEnter;
                    bool reenterUnit = false, hasFoundUnit = false ;
                    int whileLoops = 0;
                    while (!hasFoundUnit)
                    {
                        whileLoops++;
                        if (reenterUnit)
                        {
                            Console.WriteLine("Sorry, I couldn't identify a unit. Please enter the unit now.");
                            Console.WriteLine("[Kg,g,L,mL]:");
                            recievedString = Console.ReadLine();
                            recievedString = Regex.Replace(recievedString, @"[\d-]", string.Empty);
                        }
                        else
                        {
                            recievedString = Regex.Replace(recievedString, @"[\d-]", string.Empty);
                        }
                        if (iteration == 0)
                        {
                            measurementType unitType = new measurementType();
                            foreach (string sa in symbolsMass)
                            {
                                if (recievedString.ToLower() == sa.ToLower())
                                {
                                    unitType = measurementType.WEIGHT;
                                    multiplier = unitsMass[Array.IndexOf(symbolsMass, sa)];
                                    Console.WriteLine("Unit type found: Mass");
                                    hasFoundUnit = true;
                                    break;
                                }
                                else
                                {

                                }
                            }
                            if (unitType != measurementType.WEIGHT)
                            {
                                foreach (string sb in symbolsVol)
                                {
                                    if (recievedString.ToLower() == sb.ToLower())
                                    {
                                        unitType = measurementType.VOLUME;
                                        multiplier = unitsMass[Array.IndexOf(symbolsVol, sb)];
                                        Console.WriteLine("Unit type found: Mass");
                                        hasFoundUnit = true;
                                        break;
                                    }
                                }
                            }
                            if (unitType != measurementType.VOLUME && unitType != measurementType.WEIGHT)
                            {
                                reenterUnit = true;
                            }
                        }
                    }
                    
                }
                else if (forVal == "p")
                {
                    price[iteration] = valueToEnter;
                }
                else if (forVal == "i")// "i" is indicative of initialising the list variables
                {
                    // setting the arrays to the amount of products that the user will enter, with some flexibility
                    price = new float[(int)valueToEnter + 4];
                    name = new string[(int)valueToEnter + 4];
                    size = new float[(int)valueToEnter + 4];
                    score = new int[(int)valueToEnter + 4];
                }
            }
        }
        // Convenient function for a quick Horizontal Line
        static void hl()
        {
            Console.WriteLine("###################");
            Console.WriteLine();
        }
        static int[] compareRateProducts(float[] calcPrice, float[] calcSizeScore)
        {
            // setting keep variables so that there is still an array to display the 
            // variables of after scoring
            nameRetain = name;
            sizeRetain = size;
            priceRetain = price;
            int i = 0;
            foreach (string j in name)
            {
                if (name[i] != "")
                {
                    i++;
                }
                else
                {
                    break;
                }

            }
            for (int j = 0; j < i; j++)
            {
                score[calcScores(size)] += i - j;
                size[j] = 0;
                score[calcScores(price)] += j;
                price[j] = 0;
            }
            int calcScores(float[] arrayMaxToFind)
            {
                float maxValue = arrayMaxToFind.Max();
                return arrayMaxToFind.ToList().IndexOf(maxValue);
            }
            return score;
        }
    }
}