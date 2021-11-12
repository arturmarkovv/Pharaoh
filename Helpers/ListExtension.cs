using System;
using System.Collections.Generic;

namespace Pharaoh.Helpers
{
    public static class ListExtension
    {
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var rnd = new Random();
            
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
