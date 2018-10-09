using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    static class Menu {


        public static int? QueryInt() {
            while (true) {
                if (int.TryParse(Console.ReadLine(), out int output)) {
                    return output;
                }
                else {
                    return null;
                };
            }
        }



    }
}
