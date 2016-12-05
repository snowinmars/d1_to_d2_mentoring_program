using ExceptionHandling.Task1.Core;
using System;
using System.Linq;

namespace ExceptionHandling.Task1.UI
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                string input = Console.ReadLine();

                string result;

                try
                {
                    result = input.CutOff().ToString();
                }
                catch (FormatException)
                {
                    result = "Incorrect input format";
                }
                catch (IndexOutOfRangeException)
                {
                    result = "string is too short or too long";
                }
                catch (ArgumentNullException)
                {
                    result = "Something went wrong";
                }

                Console.WriteLine(result);
            }
        }
    }
}