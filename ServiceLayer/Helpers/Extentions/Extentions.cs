using ServiceLayer.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers.Extentions
{
    public static  class Extentions
    {
        public static void WriteConsole(this ConsoleColor color,string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static bool CheckSymbol(this string text)
        {
            string pattern = @"[^A-Za-z]";
            if(Regex.IsMatch(text, pattern))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + ResponseMessages.EnterAgainMessage);
                return true;
            }
            return false;
        }
    }

}
