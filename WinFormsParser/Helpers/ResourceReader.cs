using System;
using System.Globalization;

namespace GovernmentParse.Helpers
{
    public class ResourceReader
    {
        public string GetString(string str)
        {
            return Properties.Resources.ResourceManager.GetString(str);
        }

        public string GetTimeWithMs()
        {
            return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }
    }
}
