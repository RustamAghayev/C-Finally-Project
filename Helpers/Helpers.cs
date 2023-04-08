using ConsoleAppFinallyProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppFinallyProject.Helpers
{
    [Serializable]
    public partial class Helper
    {
        public static string ReadString(string caption, bool allowisNullOrEmpty = false)
        {
            string income;
        l1:
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(caption);
            Console.ForegroundColor = oldColor;

            income = Console.ReadLine();


            if (allowisNullOrEmpty == false && string.IsNullOrWhiteSpace(income))
            {
                goto l1;
            }

            return income;
        }
        public static int ReadInt(string caption)
        {
        l1:
            string income;

            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(caption);
            Console.ForegroundColor = oldColor;

            income = Console.ReadLine();


            if (!int.TryParse(income, out int value))
            {

                Console.WriteLine("Duzgun Say daxil edin");
                goto l1;
            }
            return value;
        }

            public static bool IsNameOrSurname(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsDigit(name[i]))
                {
                    return false;
                }
            }
            if (name.Length < 3)
            {
                return false;
            }
            string pattern = "^[a-zA-Z ]";
            Regex regexItem = new Regex(pattern);
            if (!regexItem.IsMatch(name))
            {
                return false;
            }

            return true;
        }

    }


}
