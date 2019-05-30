using System;
using System.Runtime;

namespace GovernmentParse.Helpers
{
    public static class GarbageCleaner
    {
        public static void ClearGarbage()
        {
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
