using ConsoleAppFinallyProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFinallyProject.Helpers
{
    public static partial class Helper
    {
        public static MenuTypes ReadEnum<T>(string caption)
            where T : Enum //generic constraint
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                
                Type uType = Enum.GetUnderlyingType(typeof(T));

                var id = Convert.ChangeType(item,uType);

                Console.WriteLine($"{id.ToString().PadLeft(2, ' ')}.{item}");

            }

            string income;
            l1:
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(caption);
            Console.ForegroundColor = oldColor;

            income = Console.ReadLine();

            if (!Enum.TryParse(typeof(MenuTypes),income,out object value) || !Enum.IsDefined(typeof(MenuTypes), value))
            {
                goto l1;
            }
            return (MenuTypes)value;
        }
    }
}
