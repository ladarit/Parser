using System.Xml;
using GovernmentParse.Models;
using HtmlAgilityPack;

namespace GovernmentParse.DataProviders
{
    public abstract class TransitionalFilesProvider
    {
        public abstract ParseResult<XmlElement> GetBlankFiles(HtmlNode row, string[] satellitePage, string controlName, bool checkBoxOption = false);
    }
}
