﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    static class Menu {
        internal static Actions QueryAction() {
            string input = Console.ReadLine();
            Console.WriteLine();
            return (Actions)int.Parse(input);
        }

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