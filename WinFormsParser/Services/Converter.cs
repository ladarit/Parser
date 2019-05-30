using System;
using System.Reflection;
using GovernmentParse.Helpers;
using HtmlAgilityPack;

namespace GovernmentParse.Services
{
    public static class Converter
    {
        private static readonly log4net.ILog Log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static HtmlDocument ConvertToHtmlDocument(string html)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);
                return document;
            }
            catch (Exception ex)
            {
                Log.Error($"ConvertToHtmlDocument.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
                return null;
            }
        }
    }
}
