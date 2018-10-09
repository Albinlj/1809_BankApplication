using System;

namespace _1809_BankApp {
    public class ConsoleQueries {
        public static string QueryString(string message) {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static int QueryInt(string message) {
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            int output;
            bool wasSuccess;
            do {
                wasSuccess = int.TryParse(Console.ReadLine(), out output);
                if (wasSuccess == false) Console.WriteLine("Not a valid integer. Try again!");
            } while (wasSuccess == false);

            Console.ForegroundColor = ConsoleColor.Gray;

            return output;
        }

        public static decimal QueryDecimal(string message) {
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            decimal output;
            bool wasSuccess;
            do {
                wasSuccess = Decimal.TryParse(Console.ReadLine(), out output);
                if (wasSuccess == false) Console.WriteLine("Not a valid integer. Try again!");
            } while (wasSuccess == false);

            Console.ForegroundColor = ConsoleColor.Gray;

            return output;
        }
    }
}