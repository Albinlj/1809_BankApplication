using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApp {
    public static class Utility {

        public static string PascalToTitlecase(string pascal) {
            return Regex.Replace(pascal, "[a-z][A-Z]", m => $"{m.Value[0]} {m.Value[1]}");
        }
        public static string PascalToSentence(string pascal) {
            return Regex.Replace(pascal, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }
    }
}
