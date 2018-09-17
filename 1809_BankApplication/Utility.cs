﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    public static class Utility {

        public static string PascalToSentence(string pascal) {
            return Regex.Replace(pascal, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");

        }
    }
}