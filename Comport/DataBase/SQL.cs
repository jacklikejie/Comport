using System.Collections.Generic;
using System.Linq;

namespace Comport.DataBase
{
    class SQL
    {
        public static string SafeText(string str)
        {
            if (str != null)
                str = str.Replace("'", "''");
            return str;
        }

        public static IEnumerable<string> SafeText(IEnumerable<string> strs)
        {
            if (strs == null)
                return null;
            else
                return strs.Select(s => SQL.SafeText(s));
        }
    }
}