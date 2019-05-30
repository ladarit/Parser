using System.Collections.Generic;
using System.Linq;
using System.Xml;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.Parsers
{
    public abstract class PageParser<T>
    {
        public readonly ResourceReader ResourceReader;

        public readonly Dictionary<string, bool> LowTableElemTypesByMultiRecordInRow;

        public readonly Dictionary<string, RecordOptions> LowRecordNamesComparator;

        public readonly Dictionary<string, RecordOptions> CommitteeRecordNamesComparator;

        public readonly Dictionary<string, RecordOptions> DeputyRecordNamesComparator;

        public readonly Dictionary<string, RecordOptions> FractionsRecordNamesComparator;

        public readonly Dictionary<string, RecordOptions> SessionPlanRecordNamesComparator;

        public readonly Dictionary<string, RecordOptions> SessionDatesRecordNamesComparator;

        public readonly Dictionary<string, string> LowElementsNames;

        public readonly Dictionary<string, string> CommitteeElementsNames;

        public readonly Dictionary<string, string> DeputyElementNames;

        public readonly Dictionary<string, string> FractionElementsNames;

        public readonly Dictionary<string, string> MonthsComparator;

        public readonly Dictionary<string, string> WeekDaysComparator;

        public readonly Dictionary<string, string> VerbalToNumbersComparator;

        protected PageParser()
        {
            ResourceReader = new ResourceReader();
            LowTableElemTypesByMultiRecordInRow = DictionaryInitializer.LowTableElemTypesByMultiRecordInRow;
            LowRecordNamesComparator = DictionaryInitializer.LowRecordNamesComparator;
            CommitteeRecordNamesComparator = DictionaryInitializer.CommitteeRecordNamesComparator;
            DeputyRecordNamesComparator = DictionaryInitializer.DeputyRecordNamesComparator;
            FractionsRecordNamesComparator = DictionaryInitializer.FractionsRecordNamesComparator;
            SessionPlanRecordNamesComparator = DictionaryInitializer.SessionPlanRecordNamesComparator;
            SessionDatesRecordNamesComparator = DictionaryInitializer.SessionDatesRecordNamesComparator;
            LowElementsNames = DictionaryInitializer.LowElementsNames;
            CommitteeElementsNames = DictionaryInitializer.CommitteeElementsNames;
            DeputyElementNames = DictionaryInitializer.DeputyElementNames;
            FractionElementsNames = DictionaryInitializer.FractionElementsNames;
            MonthsComparator = DictionaryInitializer.MonthsComparator;
            WeekDaysComparator = DictionaryInitializer.WeekDaysComparator;
            VerbalToNumbersComparator = DictionaryInitializer.VerbalToNumbersComparator;
        }

        public abstract List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<T>> satellitePageDetailsList, List<string> listOfCells = null);

        public abstract Page<T> ParseDetails(string html, bool checkBox = false, string[] satellitePages = null, string otherParams = null);

        public virtual List<XmlElement> CreateBlocks(XmlDocument doc, Dictionary<string, RecordOptions> dictionary)
        {
            List<XmlElement> blockCollection = new List<XmlElement>();
            foreach (var elem in dictionary.Select(e => e.Value.BlockName).Distinct().ToList())
            {
                XmlElement block = doc.CreateElement("block");
                block.SetAttribute("type", dictionary.FirstOrDefault(e => e.Value.BlockName == elem).Value.IsLiElement ? "list" : "record");
                block.SetAttribute("name", elem);
                blockCollection.Add(block);
            }
            return blockCollection;
        }

        /// <summary>
        /// метод добавляет в итоговую коллекцию для формирования xml данные, имеющие в поле Value один элемент
        /// </summary>
        /// <param name="details">коллекция для формирования xml</param>
        /// <param name="generalSign">значение поля GeneralSign класса Record</param>
        /// <param name="name">значение поля Name класса Record</param>
        /// <param name="value">значение поля Value класса Record</param>
        public void AddSingleValueDataToCollection(ref Page<List<string>> details, string name, string value, string generalSign = null)
        {
            details.PageDetails.Add(new Record<List<string>>
            {
                GeneralSign = generalSign,
                Name = name.RemoveOddSpaces(),
                Value = new List<string> { value.RemoveOddSpaces() }
            });
        }
    }
}
