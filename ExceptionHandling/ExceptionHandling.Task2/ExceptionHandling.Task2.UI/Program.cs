using System;

namespace ExceptionHandling.Task2.UI
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Ready");

            while (true)
            {
                string input = Console.ReadLine();

                string result;
                try
                {
                    result = Core.Core.Parse(input).ToString();
                }
                catch (ArgumentNullException)
                {
                    result = "Sorry, something went wrong";
                }
                catch (FormatException)
                {
                    result = "String have invalid format";
                }
                catch (OverflowException)
                {
                    result = "Number is too big, cant fit it to the Integer range";
                }


                Console.WriteLine(result);
            }
        }
    }
}