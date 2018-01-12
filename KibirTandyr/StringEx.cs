using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KibirTandyr
{
    public static class StringEx
    {
        // return count of matches
        public static int CountOf(this string source, char whichChar)
        {
            int count = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == whichChar)
                {
                    count++;
                }
            }
            return count;
        }

        // + return array of match positions
        public static int CountOfEx(this string source, char whichChar, out int[] pos)
        {
            pos = new int[source.Length];
            int ptr = 0;
            int count = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == whichChar)
                {
                    count++;
                    pos[ptr++] = i;
                }
            }
            return count;
        }
    }
}
