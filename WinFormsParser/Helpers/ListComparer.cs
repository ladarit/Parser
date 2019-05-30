using System.Collections.Generic;
using System.Linq;

namespace GovernmentParse.Helpers
{
    public static class ListComparer
    {
        public static bool ListEqualsTo<T>(this List<T> list1, List<T> list2)
        {
            foreach (T elem in list1)
                if (!list2.Select(x => x.Equals(elem)).Any())
                    return false;
            return true;
        }
    }
}
