using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace L2DigiProgrammingAssesment
{
    class V5
    {
        static string[] name, nameRetain;
        static float[] price, size, priceRetain, sizeRetain;
        static public int[] score;

        public enum measurementType
        {
            NULLVALUE,
            WEIGHT,
            VOLUME
        }
        static measurementType unitTypeControl = new measurementType(); // create new instance of measurementtype enum to use later
        static measurementType unitTypeGiven = new measurementType(); // create second intance of measurementtype enum for checking if
        // the user's input is valid against the previous ones (if that makes sense)
        public static void V5Main()
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

                Console.WriteLine($"Price {totI + 1}:");
                Console.Write("$"); // inserting a "$" before the user's input, prompting them to input only the number itself.
                inputValid(totI, "p");
                Console.WriteLine("Please enter all sizes in the same unit type e.g: kg & g, L & mL");
                Console.WriteLine($"Size {totI + 1}:");
                inputValid(totI, "s");
                totI++;
                Console.WriteLine();
                if (totI >= name.Length - 5 && name.Length - totI > 1)
                {
                    Console.WriteLine("You have entered the amount of products that you indicated.");
                    Console.WriteLine($"Do you need to add more? You have {name.Length - totI - 1} more available");
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
                else if (name.Length - totI > 1)
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
            string unitsUsed;
            if (unitTypeControl == measurementType.VOLUME)
            {
                unitsUsed = "L";
            }
            else if (unitTypeControl == measurementType.WEIGHT)
            {
                unitsUsed = "Kg";
            }
            else
            {
                unitsUsed = "Units";
            }
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != null)
                {
                    priceRetain[i] = price[i];
                    sizeRetain[i] = size[i]; // setting retainable values for display after they are deleted during score system.
                    Console.WriteLine($"{i + 1}) {name[i]}> Price: ${price[i]}, Size: {size[i]}{unitsUsed}");
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
                    Console.WriteLine($"{i + 1}) {name[indexTopScore]}> Price: ${priceRetain[indexTopScore]}, size: {sizeRetain[indexTopScore]}{unitsUsed}, Score: {returnScore[indexTopScore]}pts.");
                    score[indexTopScore] = -2; // ensuring that the same product's score won't be called twice by setting it negative.
                }
            }
        }
        /*********************************************************************
         * Method:      inputValid
         * Purpose:     This method takes all inputs other than input for names,
         *              ensures they are put into their respective variables as
         *              valid values. It works differently depending on which 
         *              variable it is validating for. e.g for size it takes
         *              all of the users input, seperates it into unit and
         *              numerical values, and validates from there, whereas the
         *              pricing variable validation simply checks the number 
         *              value.
         *********************************************************************/
        static void inputValid(int iteration, string forVal)
        {
            bool sizeEnteredExclusive = false;
            string tmpVal, keepVal, tmpUnitDisplay;
            while (true)
            {
                if (!sizeEnteredExclusive)
                {
                    tmpVal = Console.ReadLine();
                    keepVal = tmpVal;
                    if (float.TryParse(tmpVal, out float parsed))
                    {
                        if (parsed > 1000000000)
                        {
                            Console.WriteLine("Sorry, the value must be less than 1,000,000,000. Please try again.");
                        } else if (parsed < 0.001 && parsed >= 0)
                        {
                            Console.WriteLine("Sorry, the absolute value must be greater than 0.001. Please try again.");
                        }
                        else
                        {
                            valueInput(Math.Abs(parsed), tmpVal);
                            Console.WriteLine("Value submitted");
                            break;
                        }
                        
                    }

                    else
                    {
                        tmpUnitDisplay = Regex.Replace(tmpVal, @"[\d-.]", string.Empty);
                        tmpVal = Regex.Replace(tmpVal, "[^0-9.]", ""); // attempts to filter the input value into numerical if the recieved value was not parseable.
                        if (float.TryParse(tmpVal, out float parsed2))
                        {
                            if (parsed2 > 1000000000)
                            {
                                Console.WriteLine("Sorry, the value I found was incorrect, value must be less than 1,000,000,000. Please try again.");
                            }
                            else if (parsed2 < 0.001 && parsed2 >= 0)
                            {
                                Console.WriteLine("Sorry, the value I found was incorrect, absolute value must be greater than 0.001. Please try again.");
                            }
                            else
                            {
                                if (forVal != "s") // if the value that will be entered (Size, Price etc) is size, it needs a
                                                   // different format of communication, as more data is required for that function, thus the
                                                   // if statement allows different writes per condition.
                                {
                                    Console.WriteLine($"Sorry, I didn't understand that, did you mean: \"{parsed2}\"? [y/n]");
                                    string yn;
                                    yn = Console.ReadLine();
                                    if (yn == "y")
                                    {
                                        valueInput(Math.Abs(parsed2), keepVal);
                                        Console.WriteLine("Value submitted");
                                        break;
                                    }
                                    else if (yn == "n")
                                    {
                                        Console.WriteLine("Okay, please try again.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("An error occured, please try again:"); // this is a final wall, in case any of the other filters or 
                                                                                                  // requests fail, this will serve as a stop to prevent program failure.
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Confirm that you would like to enter numerical value {parsed2} and unit {tmpUnitDisplay}?");

                                    while (true) // loops until a valid input is given
                                    {
                                        Console.WriteLine("[y/n]");
                                        string yn = Console.ReadLine();
                                        if (yn == "y")
                                        {
                                            valueInput(Math.Abs(parsed2), keepVal);

                                            break;
                                        }
                                        else if (yn == "n")
                                        {
                                            Console.WriteLine("Okay, please try again");
                                            break;
                                        }

                                    }

                                }
                            }
                            

                        }
                        else
                        {
                            Console.WriteLine("Sorry, I couldn't read that as a number value, please enter a number input.");
                        }
                    }
                }
                else if (sizeEnteredExclusive)
                {
                    Console.WriteLine("Value submitted");
                    break;
                }

            }
            /*************************************************************************
             * Method:      valueInput
             * Purpose:     The purpose of valueInput is to take the validated input
             *              inputValid, and allocate the given values to their respective
             *              variables. It uses an identifier token, forVal, to decide 
             *              where the value (valueToEnter) should go.
             *************************************************************************/
            void valueInput(float valueToEnter, string recievedString)
            {
                string[] symbolsMass = new string[] { "kg", "g" };
                int[] unitsMass = new int[] { 1000, 1 };

                string[] symbolsVol = new string[] { "l", "ml" };
                int[] unitsVol = new int[] { 1000, 1 };
                int multiplier = 1;

                if (forVal == "s")
                {

                    bool reenterUnit = false, hasFoundUnit = false, noUnit = false;
                    int whileLoops = 0;

                    while (!hasFoundUnit)
                    {
                        whileLoops++;
                        if (reenterUnit)
                        {
                            if (noUnit || whileLoops == 2 /* this second check is for if the original unit entry was invalid */)
                            {
                                Console.WriteLine("Sorry, I couldn't identify a unit. Please enter the unit now.");
                            }
                            Console.WriteLine("[Kg,g,L,mL]:");
                            recievedString = Console.ReadLine().ToLower();
                        }
                        recievedString = Regex.Replace(recievedString, @"[\d-.]", string.Empty);
                        

                        if (iteration == 0)
                        {
                            foreach (string sa in symbolsMass)
                            {
                                if (recievedString.ToLower() == sa.ToLower())
                                {
                                    unitTypeControl = measurementType.WEIGHT;
                                    multiplier = unitsMass[Array.IndexOf(symbolsMass, sa)];
                                    sizeEnteredExclusive = true;
                                    hasFoundUnit = true;
                                    reenterUnit = false;
                                    break;
                                }
                            }
                            if (unitTypeControl != measurementType.WEIGHT)
                            {
                                foreach (string sb in symbolsVol)
                                {
                                    if (recievedString.ToLower() == sb.ToLower())
                                    {
                                        unitTypeControl = measurementType.VOLUME;
                                        multiplier = unitsMass[Array.IndexOf(symbolsVol, sb)];
                                        sizeEnteredExclusive = true;
                                        hasFoundUnit = true;
                                        reenterUnit = false;
                                        break;
                                    }
                                }
                            }
                            if (unitTypeControl != measurementType.VOLUME && unitTypeControl != measurementType.WEIGHT)
                            {
                                reenterUnit = true;
                            }
                        }
                        else if (iteration > 0)
                        {
                            foreach (string sa in symbolsMass)
                            {
                                if (sa == recievedString)
                                {
                                    unitTypeGiven = measurementType.WEIGHT;
                                    break;
                                }
                            }
                            foreach (string sb in symbolsVol)
                            {
                                if (sb == recievedString)
                                {
                                    unitTypeGiven = measurementType.VOLUME;
                                    break;
                                }
                            }
                            if (unitTypeGiven == unitTypeControl)
                            {
                                sizeEnteredExclusive = true;
                                hasFoundUnit = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Oops! You need to enter each product in the same unit type e.g mass (kg, g), volume (L,mL)");
                                Console.WriteLine("Please enter the unit again now");
                                reenterUnit = true;

                            }

                        }
                    }
                    size[iteration] = convertToLargeUnits(recievedString, valueToEnter);
                }
                else if (forVal == "p")
                {
                    price[iteration] = valueToEnter;
                }
                else if (forVal == "i")// "i" is indicative of initialising the list variables
                {
                    // setting the arrays to the amount of products that the user will enter, with some flexibility
                    price = new float[(int)valueToEnter + 5];
                    priceRetain = new float[(int)valueToEnter + 5];
                    name = new string[(int)valueToEnter + 5];
                    size = new float[(int)valueToEnter + 5];
                    sizeRetain = new float[(int)valueToEnter + 5];
                    score = new int[(int)valueToEnter + 5];
                }
            }
        }
        /**************************************************************************
         * Method:      convertToLargeUnits
         * Purpose:     To take all sizes given to the method, and convert the value
         *              to the largest possible unit, e.g if a value of unit "g" and 
         *              value 2500 is entered, the value will be converted to unit
         *              "Kg" and value 2.5. This means that the scoring comparison will
         *              be comparing sizes of the same unit, so grams won't be graded as
         *              though they were kg.
         **************************************************************************/
        static float convertToLargeUnits(string inputUnit, float unconvertedValue)
        {
            if (unitTypeControl == measurementType.VOLUME)
            {
                if (inputUnit.ToLower() == "ml")
                {
                    return unconvertedValue / 1000;
                }
                else if (inputUnit.ToLower() == "l")
                {
                    return unconvertedValue;
                }
                else
                {
                    return 1;
                }
            }
            else if (unitTypeControl == measurementType.WEIGHT)
            {
                if (inputUnit.ToLower() == "g")
                {
                    return unconvertedValue / 1000;
                }
                else if (inputUnit.ToLower() == "kg")
                {
                    return unconvertedValue;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 3;
            }
        }
        // Convenient function for a quick Horizontal Line
        static void hl()
        {
            Console.WriteLine("###################");
            Console.WriteLine();
        }
        /*******************************************************************
         * Method:      compareRateProducts
         * Purpose:     To calculate the final scores of the entered products,
         *              allocating points based on category rankings.
         *******************************************************************/
        static int[] compareRateProducts(float[] calcPrice, float[] calcSizeScore)
        {
            // setting keep variables so that there is still an array to display the 
            // variables of after scoring
            int i = 0, indexNoSize = 0, indexNoPrice = 0;
            foreach (string j in name)
            {
                if (name[i] != "" && name[i] != null)
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
                indexNoSize = calcScores(size);
                indexNoPrice = calcScores(price);

                score[indexNoSize] += i - j; // Adding i-j means largest valid size gets best points
                size[indexNoSize] = 0;
                score[indexNoPrice] += j; // Adding j rather than i-j means that the lowest valid price will get best points
                price[indexNoPrice] = 0;
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

