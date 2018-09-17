using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    static class Output {
        public static void PrintOptions() {
            for (int i = 0; i < Enum.GetNames(typeof(Actions)).Length; i++) {
                Console.WriteLine($"{i}) {Utility.PascalToSentence(Enum.GetName(typeof(Actions), i))}");
            }
            Console.WriteLine();
        }
    }
}
