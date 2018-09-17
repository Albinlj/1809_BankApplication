using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    static class Input {
        internal static Actions Query() {
            Output.PrintOptions();
            Console.ReadLine();
            return Actions.SearchCustomer;


        }

        public static int? QueryInt() {
            int output = 0;
            while (true) {
                if (int.TryParse(Console.ReadLine(), out output)) {
                    return output;
                }
                else {
                    return null;
                };
            }
        }

    }
}
