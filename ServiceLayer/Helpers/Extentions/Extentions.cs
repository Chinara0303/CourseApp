using ServiceLayer.Helpers.Constants;
using System.Text.RegularExpressions;

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
        public static bool CheckNumEqualZero(this int id)
        {
            if (id == 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EqualToZeroMessage + ResponseMessages.EnterAgainMessage);
                return true;
            }
            return false;
        } 
    }

}
