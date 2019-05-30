using System;
using System.Collections.Generic;
using System.Xml;
using GovernmentParse.Models;

namespace GovernmentParse.DataProviders
{
    public static class DictionaryInitializer
    {
        //сопоставление и определение имен блоков и элементов
        public static Dictionary<string, RecordOptions> LowRecordNamesComparator;

        public static Dictionary<string, RecordOptions> CommitteeRecordNamesComparator;

        public static Dictionary<string, RecordOptions> SessionPlanRecordNamesComparator;

        public static Dictionary<string, RecordOptions> SessionDatesRecordNamesComparator;

        public static Dictionary<string, RecordOptions> DeputyRecordNamesComparator;

        public static Dictionary<string, RecordOptions> FractionsRecordNamesComparator;

        //имена элементов
        public static Dictionary<string, string> LowElementsNames;

        public static Dictionary<string, string> CommitteeElementsNames;

        public static Dictionary<string, string> DeputyElementNames;

        public static Dictionary<string, string> FractionElementsNames;

        public static Dictionary<string, string> SessionElementNames;

        //имена файлов
        public static Dictionary<string, string> FirstPartOfFileName;

        public static Dictionary<string, Func<XmlNodeList, string>> SecondPartOfFileName;

        public static Dictionary<string, string> FileNamesComparator;

        public static Dictionary<string, string> FileNamesComparatorRevis;

        //вспомогат. словари
        public static Dictionary<string, string> MonthsComparator;

        public static Dictionary<string, string> WeekDaysComparator;

        public static Dictionary<string, string> VerbalToNumbersComparator;

        public static Dictionary<string, bool> LowTableElemTypesByMultiRecordInRow;

        public static Dictionary<string, string> VotingDictionary;

        public static Dictionary<string, string> BtnMessageDictionary;

        public static Dictionary<string, List<DateTime>> ConvocationDates;

        static DictionaryInitializer()
        {
            LowTableElemTypesByMultiRecordInRow = new Dictionary<string, bool>
            {
                { "Сесія реєстрації", false },
                { "Включено до порядку денного", false },
                { "Редакція законопроекту", false },
                { "Рубрика законопроекту", false },
                { "Суб'єкт права законодавчої ініціативи", false},
                { "Ініціатор законопроекту", false },
                { "Головний комітет", false },
                { "Інші комітети", false },
                { "Проходження законопроекту", false },
                { "Документи, пов'язані із роботою", true },
                //{ "Проходження", true },
                //{ "Опрацювання комітетами", true },
                //{ "Результати голосування", true }
            };

            LowElementsNames = new Dictionary<string, string>
            {
                {"Скорочений текст", "ShortText" },
                {"Формалізований текст", "FormalizedText" },
                {@"\d+\.\d+\.\d+", "Date" },
                {"Передано:", "Transferred" },
                {"Отримано:", "Received" },
                { "Дата голосування:", "VoteDate" },
                { "Найменування голосування:", "VoteName" },
                { "За:", "VotedForALaw" },
                { "Проти:", "VotedAgainstALaw" },
                { "Утрималось:", "RestrainedFromVoting" },
                { "Не голосувало:", "DidNotVote" },
                { "Відсутньо:", "Absent" },
                { "Рішення:", "Sulotion" },
                { "Кількість депутатів:", "Count" },
                { "Ім'я:", "Name" },
                { "Назва фракції:", "Fraction" },
                { "Назва:", "Conclusion" },
                { "URL голосування:", "URL" }
            };

            CommitteeElementsNames = new Dictionary<string, string>
            {
                { "Ім'я", "Name" },
                { "Посада", "Position" },
                { "Повна посада", "FullPosition" },
                { "Тел", "Telephone" },
                { "Факс", "Fax" },
                { "Ел.пошта", "Email" },

                { "Номер файлу", "FileNumver" },
                { "Тема засідання", "Theme" },
                { "Номер законопроекта", "LowNumber" },
                { "Інше питання", "OtherQuestion" },
                { "Назва комітету", "CommitteeName" },
                { "Адреса", "Adress" },
                { "Початкова дата", "StartDate" },
                { "Кінцева дата", "StartDate" }
            };

            DeputyElementNames = new Dictionary<string, string>
            {
                { "Назва фракції", "FractionName"},
                { "Дата вступу:", "DateOfEntry"},
                { "Дата виходу:", "DateOfLeaving"},
                { "Посада:", "Post"},
                { "Дата прийому:", "DateOfAppointment"},
                { "Дата звільнення:", "DateOfDismissal"},
                { "Назва голосування:", "VotingName"},
                { "Рішення:", "Sulotion"},
                { "Дата голосування:", "VotingDate"},
                { "№ та дата запиту, автор:", "AuthorAndDate"},
                { "До кого запит:", "ToWhom"},
                { "Зміст запиту:", "QueryContent"},
                { "Стан виконання:", "PerformanceStatus"},
                { "№ реєстр.проекту:", "LawRegisterNumber"},
                { "Дата реєстрації:", "RegistrationDate"},
                { "Назва законопроекту:", "LawName"},
                { "Став чинним актом:", "BecameAValidAct"},
                { "Заголовок таблиці поправок:", "titleOfTheTableOfAmendments"},
                { "Читання / Дата розгляду:", "DateOfReading"},
                { "Всього:", "TotalCount"},
                { "Врах.:", "CreditedCount"},
                { "Відх.:", "RejectedCount"},
                { "Врах. част.:", "CreditedPartiallyCount"},
                { "Врах. ред.:", "CreditedRedactCount"},
                { "Інше:", "Other"},
                { "Немає висн.:", "NoConclusion"},
                { "Про що виступ:", "SpeechAbout"},
                { "Дата виступу:", "SpeechDate"},
                { "Час виступу:", "SpeechTime"},
                { "Тема виступу:", "speechTheme"},
                { "Місце виступу:", "speechPlace"},
                { "Ініціатор теми виступу:", "speechThemeInitiator"},

                //фракції
                { "БЛОК ПЕТРА ПОРОШЕНКА", "Фракція ПАРТІЇ \"БЛОК ПЕТРА ПОРОШЕНКА\""},
                { "НАРОДНИЙ ФРОНТ", "Фракція Політичної партії \"НАРОДНИЙ ФРОНТ\""},
                { "Опозиційний блок", "Фракція Політичної партії \"Опозиційний блок\" у Верховній Раді України восьмого скликання"},
                { "САМОПОМІЧ", "Фракція Політичної партії \"Об\'єднання \"САМОПОМІЧ\""},
                { "Радикальної партії Олега Ляшка", "Фракція Радикальної партії Олега Ляшка"},
                { "Батьківщина", "Фракція політичної партії \"Всеукраїнське об\'єднання \"Батьківщина\" у Верховній Раді України"},
                { "Воля народу", "Група \"Воля народу\""},
                { "Відродження", "Група \"Партія \"Відродження\""},
                { "Не входить до складу будь-якої фракції", "Народні депутати, які не входять до складу жодної фракції чи групи"}
            };

            FractionElementsNames = new Dictionary<string, string>
            {
                { "Ім'я", "Name" },
                { "ПІБ", "Name" },
                { "Посада", "Position" },
                { "Дата вступу", "DateOfEntry" },
                { "Дата виходу", "DateOfLeaving" }
            };

            SessionElementNames = new Dictionary <string, string>
            {
                { "Ім'я", "Name" },
                { "ПІБ", "Name" },
                { "Посада", "Position" },
                { "Дата вступу", "DateOfEntry" },
                { "Дата виходу", "DateOfLeaving" }
            };

            LowRecordNamesComparator = new Dictionary<string, RecordOptions>
            {
                { "Рубрика законопроекту", new RecordOptions { ElementName = "RubricOfTheLow", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Сесія реєстрації", new RecordOptions { ElementName = "SessionOfRegistration", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Редакція законопроекту", new RecordOptions { ElementName = "RevisionOfTheLow", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Суб'єкт права законодавчої ініціативи", new RecordOptions { ElementName = "Subject", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Включено до порядку денного за номером", new RecordOptions { ElementName = "IncludedInTheAgendaNumber", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Включено до порядку денного за датою", new RecordOptions { ElementName = "IncludedInTheAgendaDate", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Номер акту", new RecordOptions { ElementName = "ActNumber", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата акту", new RecordOptions { ElementName = "ActDate", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Адреса веб-сторінки", new RecordOptions { ElementName = "LawURL", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Проходження законопроекту", new RecordOptions { ElementName = "Proceed", BlockName = "LawInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Ініціатор законопроекту", new RecordOptions { ElementName = "Initiators", BlockName = "Initiators" , IsLiElement = true, IsMultiRecordInRow = false } },
                { "Головний комітет", new RecordOptions { ElementName = "MainCommittee", BlockName = "MainCommittee" , IsLiElement = false, IsMultiRecordInRow = false } },
                //{ "Інші комітети", new RecordOptions { ElementName = "OtherCommittees", BlockName = "OtherCommittees" , IsLiElement = true, IsMultiRecordInRow = false } },
                { "Текст законопроекту та супровідні документи", new RecordOptions { ElementName = "TextOfTheLow", BlockName = "TextOfTheLow" , IsLiElement = true, IsMultiRecordInRow = false } },
                { "Документи, пов'язані із роботою", new RecordOptions { ElementName = "DocumentsRelatedToWork", BlockName = "DocumentsRelatedToWork" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Conclusion" } },
                { "Проходження", new RecordOptions { ElementName = "LawType", BlockName = "LawType" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Опрацювання комітетами", new RecordOptions { ElementName = "CommitteesProcessing", BlockName = "CommitteesProcessing" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                { "Результати голосування", new RecordOptions { ElementName = "VotingResults", BlockName = "VotingResults" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },

                //{ "Поіменні результати голосування", new RecordOptions { ElementName = "VotingResults", BlockName = "NamedVotingResults" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                { "Пофракційні результати голосування", new RecordOptions { ElementName = "FractionResult", BlockName = "FractionResult", IsChildBlock = true, IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                { "Депутатські результати голосування", new RecordOptions { ElementName = "DeputyResult", BlockName = "DeputyResult", IsChildBlock = true, IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },

                //парламентские фракции
                //{ "Фракція ПАРТІЇ \"БЛОК ПЕТРА ПОРОШЕНКА\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "BPPFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Фракція Політичної партії \"НАРОДНИЙ ФРОНТ\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "NarodniyFrontFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Позафракційні", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "NonFractional" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Фракція Політичної партії \"Опозиційний блок\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "OppoBlockFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Група \"Партія \"Відродження\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "VidrodzhennyaFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Фракція Політичної партії \"Об’єднання \"САМОПОМІЧ\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "SamopomichFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Фракція політичної партії \"Всеукраїнське об’єднання \"Батьківщина\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "BatkivshchynaFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Фракція Радикальної партії Олега Ляшка", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "RadykalnaPartiyaFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } },
                //{ "Група \"Воля народу\"", new RecordOptions { ElementName = "VotingResultsByFractions", BlockName = "VolyaNaroduFraction" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Name" } }
            };

            CommitteeRecordNamesComparator = new Dictionary<string, RecordOptions>
            {
                { "Назва комітету", new RecordOptions { ElementName = "CommitteeName", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Скликання", new RecordOptions { ElementName = "Convocation", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Посилання", new RecordOptions { ElementName = "CommitteeURL", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата створення", new RecordOptions { ElementName = "DateOfCreation", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Підстава для створення", new RecordOptions { ElementName = "CauseOfCreation", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Підстава для створення за датою", new RecordOptions { ElementName = "CauseOfCreationDate", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Кількісний склад", new RecordOptions { ElementName = "Quantity", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Веб-сайт Комітету", new RecordOptions { ElementName = "WebSite", BlockName = "CommitteeInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Склад комітету", new RecordOptions { ElementName = "Participants", BlockName = "Participants" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Секретаріат", new RecordOptions { ElementName = "Secretariate", BlockName = "Secretariate" , IsLiElement = true, IsMultiRecordInRow = true, DefRowName = "Position" } },
                { "Засідання комітету", new RecordOptions { ElementName = "CommitteeWork", BlockName = "CommitteeWork" , IsLiElement = false, IsMultiRecordInRow = true, DefRowName = "Number" } }
            };

            SessionPlanRecordNamesComparator = new Dictionary<string, RecordOptions>
            {
                { "Реєстр. номер", new RecordOptions { ElementName = "RegistrationNumber", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Суб'єкт ініціативи", new RecordOptions { ElementName = "SubjectOfTheInitiative", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Назва законопроекту (питання)", new RecordOptions { ElementName = "LawOrQuestionName", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Відповідальні за підготовку", new RecordOptions { ElementName = "ResponsibleForPreparative", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Дата", new RecordOptions { ElementName = "Date", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Назва дня", new RecordOptions { ElementName = "DayName", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Пленарний тиждень", new RecordOptions { ElementName = "PlanarWeek", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Номер читання", new RecordOptions { ElementName = "PhaseOfReading", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Година", new RecordOptions { ElementName = "Time", BlockName = "Week" , IsLiElement = true, IsMultiRecordInRow = true } }
            };

            SessionDatesRecordNamesComparator = new Dictionary<string, RecordOptions>
            {
                { "Номер сесії", new RecordOptions { ElementName = "SessionNumber", BlockName = "SessionDatesInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Номер скликання", new RecordOptions { ElementName = "Convocation", BlockName = "SessionDatesInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата", new RecordOptions { ElementName = "Date", BlockName = "SessionDates" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Назва дня", new RecordOptions { ElementName = "DayName", BlockName = "SessionDates" , IsLiElement = true, IsMultiRecordInRow = true } }
            };

            DeputyRecordNamesComparator = new Dictionary<string, RecordOptions>
            {
                //файлы общей информации о депутате
                { "Ім'я", new RecordOptions { ElementName = "Name", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Скликання", new RecordOptions { ElementName = "Convocation", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Обраний по:", new RecordOptions { ElementName = "ElectedBy", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Партія:", new RecordOptions { ElementName = "Party", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Регіон:", new RecordOptions { ElementName = "Region", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Номер у списку:", new RecordOptions { ElementName = "NumberInParty", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата набуття депутатських повноважень:", new RecordOptions { ElementName = "DateOfReceiptsOfDeputyPower", BlockName = "GeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата припинення депутатських повноважень:", new RecordOptions { ElementName = "DateOfTerminationOfDeputyPower", BlockName = "GeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Причина дострокового припинення депутатських повноважень:", new RecordOptions { ElementName = "CauseOfTerminationOfDeputyPower", BlockName = "GeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Фракція:", new RecordOptions { ElementName = "Fraction", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Посада у фракції:", new RecordOptions { ElementName = "PostInFraction", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата народження:", new RecordOptions { ElementName = "BirthDay", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Відомості на момент обрання:", new RecordOptions { ElementName = "AddittionalInfo", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Відомості про партійність:", new RecordOptions { ElementName = "PartyInfo", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Посилання", new RecordOptions { ElementName = "DeputyURL", BlockName = "GeneralInfo", IsLiElement = false, IsMultiRecordInRow = false } },
                { "Переходи по фракціях", new RecordOptions { ElementName = "TransitionsInFractions", BlockName = "TransitionsInFractions", IsLiElement = true, IsMultiRecordInRow = true } },
                { "Посади протягом скликання", new RecordOptions { ElementName = "PositionsDuringTheConvocation", BlockName = "PositionsDuringTheConvocation", IsLiElement = true, IsMultiRecordInRow = true } },
                
                //файлы голосований депутата
                { "Ім'я для голосування", new RecordOptions { ElementName = "Name", BlockName = "DeputyNameForVoting" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Голосування", new RecordOptions { ElementName = "Voting", BlockName = "Voting" , IsLiElement = true, IsMultiRecordInRow = true } },
                
                //файлы запросов депутата
                { "Ім'я для запиту", new RecordOptions { ElementName = "Name", BlockName = "DeputyNameForQuery" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "I. Президенту України", new RecordOptions { ElementName = "Voting", BlockName = "QueryToPresident" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "II. Голові Верховної Ради України, керівникам комітетів Верховної Ради України", new RecordOptions { ElementName = "Voting", BlockName = "QueryToRadaHead" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "III. Кабінету Міністрів України", new RecordOptions { ElementName = "Voting", BlockName = "QueryToKmu" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "IV. Керівникам міністерств і відомств України", new RecordOptions { ElementName = "Voting", BlockName = "QueryToHeadersOfMinistry" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "V. Генеральному прокурору України", new RecordOptions { ElementName = "Voting", BlockName = "QueryToProcurator" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "VI. Місцевим органам влади і управління", new RecordOptions { ElementName = "Voting", BlockName = "QueryToLocalManagment" , IsLiElement = true, IsMultiRecordInRow = true } },
                
                //файлы законотворческой активности депутата 
                { "Ім'я для законтв. діяльності", new RecordOptions { ElementName = "Name", BlockName = "DeputyNameForLawCreate" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Законопроекти, подані суб'єктом права законодавчої ініціативи", new RecordOptions { ElementName = "Voting", BlockName = "FilingLaws" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Перелік таблиць поправок, до яких були подані пропозиції", new RecordOptions { ElementName = "Voting", BlockName = "RedactedTableList" , IsLiElement = true, IsMultiRecordInRow = true } },
                
                //файлы выступлений депутата
                { "Ім'я для виступу", new RecordOptions { ElementName = "Name", BlockName = "DeputyNameForSpeech" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Виступи", new RecordOptions { ElementName = "Speech", BlockName = "Speech" , IsLiElement = true, IsMultiRecordInRow = true } }
            };

            FractionsRecordNamesComparator = new Dictionary<string, RecordOptions>
            {
                { "Назва фракції", new RecordOptions { ElementName = "FractionName", BlockName = "FractionGeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Дата створення", new RecordOptions { ElementName = "DateOfCreation", BlockName = "FractionGeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Кількісний склад", new RecordOptions { ElementName = "Quantity", BlockName = "FractionGeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Скликання", new RecordOptions { ElementName = "Convocation", BlockName = "FractionGeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Посилання", new RecordOptions { ElementName = "FractionURL", BlockName = "FractionGeneralInfo" , IsLiElement = false, IsMultiRecordInRow = false } },
                { "Склад фракції", new RecordOptions { ElementName = "Participants", BlockName = "Participants" , IsLiElement = true, IsMultiRecordInRow = true } },
                { "Динаміка переходів", new RecordOptions { ElementName = "DynamicOfTransitions", BlockName = "DynamicOfTransitions" , IsLiElement = true, IsMultiRecordInRow = true } }
            };

            FirstPartOfFileName = new Dictionary<string, string>
            {
                {"LawInfo", "Законопроект" },
                {"CommitteeInfo", "Комітет" },
                {"GeneralInfo", "Депутат#Інфо" },
                {"DeputyNameForVoting", "Депутат#Голосування" },
                {"FractionGeneralInfo", "Фракція" },

                {"VoteInfo", "Голосування" },
                //{"BPPFraction", "Голосування" },
                //{"NarodniyFrontFraction", "Голосування" },
                //{"NonFractional", "Голосування" },
                //{"OppoBlockFraction", "Голосування" },
                //{"VidrodzhennyaFraction", "Голосування" },
                //{"SamopomichFraction", "Голосування" },
                //{"BatkivshchynaFraction", "Голосування" },
                //{"RadykalnaPartiyaFraction", "Голосування" },
                //{"VolyaNaroduFraction", "Голосування" },

                {"Week","Засідання" },
                {"DeputyNameForQuery", "Депутат#Запит" },
                {"QueryToPresident","Депутат#Запит" },
                {"QueryToRadaHead","Депутат#Запит" },
                {"QueryToKmu","Депутат#Запит" },
                {"QueryToHeadersOfMinistry","Депутат#Запит" },
                {"QueryToProcurator","Депутат#Запит" },
                {"QueryToLocalManagment","Депутат#Запит" },
                {"FilingLaws", "Депутат#ЗаконДіяльність" },
                {"RedactedTableList", "Депутат#ЗаконДіяльність" },
                {"DeputyNameForLawCreate", "Депутат#ЗаконДіяльність" },
                {"DeputyNameForSpeech", "Депутат#Виступи" },
                {"CommitteeWork", "Комітет#Графік" },
                {"SessionDatesInfo", "Засідання#Графік" }
            };

            SecondPartOfFileName = new Dictionary<string, Func<XmlNodeList, string>>
            {
                { @"^Голосування", list => list.Item(0)?.Attributes?[2].Value },
                { @"^Засідання$|^Депутат#ЗаконДіяльність|^Законопроект", list => list.Item(0)?.FirstChild.InnerText },
                { @"^(Фракція|Комітет|Депутат#Інфо)", list => list.Item(0)?.FirstChild.InnerText + "#" + list.Item(0)?.ChildNodes[1].InnerText },
                { @"^Депутат#(Запит|Голосування|Виступи)", list => list.Item(0)?.FirstChild.InnerText + "#" + list.Item(1)?.FirstChild.FirstChild.InnerText },
                { @"^Засідання#Графік", list => list.Item(0)?.FirstChild.InnerText + "#" + list.Item(0)?.LastChild.InnerText }
            };

            FileNamesComparator = new Dictionary<string, string>
            {
                { @"^Законопроект", "NewLawNumber"},
                { @"^\d+.*", "NewDocumentName"},
                { @"^Комітет", "NewCommiteeName"},
                { @"^Депутат#Інфо", "NewDeputy"},
                { @"^Депутат#Фото", "NewDeputyPhoto"},
                { @"^Фракція", "NewFraction"},
                { @"^Засідання", "NewSessionPlan"},
                { @"^Голосування#Документ", "NewVotingRtfDoc"},
                { @"^Голосування#\d+", "NewVotingXmlDoc"},
                //форматы файлов
                { ".xml", ".xml"},
                { ".rtf", ".rtf"},
                { ".doc", ".doc"},
                { ".docx", ".docx"},
                { ".xls", ".xls"},
                { ".xlsx", ".xlsx"},
                { ".ppt", ".ppt"},
                { ".pptx", ".pptx"},
                { ".pdf", ".pdf"},
                { ".jpg", ".jpg"},
                { ".rar", ".rar"},
                { ".zip", ".zip"},
                { ".7z", ".7z"}
            };

            FileNamesComparatorRevis = new Dictionary<string, string>
            {
                { @"^Законопроект", "UpdatedLawNumber"},
                { @"^\d+.*", "UpdatedDocumentName"},
                { @"^Комітет", "UpdatedCommiteeName"},
                { @"^Депутат#Інфо", "UpdatedDeputy"},
                { @"^Депутат#Фото", "UpdatedDeputyPhoto"},
                { @"^Фракція", "UpdatedFraction"},
                { @"^Засідання", "UpdatedSessionPlan"},
                { @"^Голосування#Документ", "VotingRtfDoc"},
                { @"^Голосування#\d+", "VotingXmlDoc"}
            };

            MonthsComparator = new Dictionary<string, string>
            {
                { "січ", "01"},
                { "лют" , "02"},
                { "берез", "03"},
                { "квіт", "04"},
                { "трав", "05"},
                { "черв", "06"},
                { "лип", "07"},
                { "серп", "08"},
                { "верес", "09"},
                { "жовт", "10"},
                { "листоп", "11"},
                { "груд", "12"}
            };

            WeekDaysComparator = new Dictionary<string, string>
            {
                { "пн", "Понеділок"},
                { "вт", "Вівторок"},
                { "ср", "Середа"},
                { "чт", "Четвер"},
                { "пт", "П'ятниця"},
                { "сб", "Субота"},
                { "нд", "Неділя"}
            };

            VerbalToNumbersComparator = new Dictionary<string, string>
            {
                { @"^((О|о)дин\s+|(П|п)ерш)", "1"},
                { @"^(Д|д)(ва\s+|руг)",       "2"},
                { @"^(Т|т)р(и\s+|ет)",        "3"},
                { @"^(Ч|ч)(отири\s+|етверт)", "4"},
                { @"^(П|п)'ят(а|е|ий|ь)\s+",  "5"},
                { @"^(Ш|ш)(ість\s+|ост)",     "6"},
                { @"^(С|с)(ім\s+|ьом)",       "7"},
                { @"^(В|в)(ісім\s+|осьм)",    "8"},
                { @"^(Д|д)ев'ят(а|е|ий|ь)\s+","9"},
                { @"^(Д|д)есят",             "10"},
                { @"^(О|о)динадц",           "11"},
                { @"^(Д|д)ванадц",           "12"},
                { @"^(Т|т)ринадц",           "13"},
                { @"^(Ч|ч)отирнадц",         "14"},
                { @"^(П|п)'ятнадц",          "15"},
                { @"^(Ш|ш)істнадц",          "16"},
                { @"^(С|с)імнадц",           "17"},
                { @"^(В|в)ісімнадц",         "18"},
                { @"^(Д|д)ев'ятнадц",        "19"},
                { @"^(Д|д)вадцят",           "20"}
            };

            BtnMessageDictionary = new Dictionary<string, string>
            {
                {"SaveLawsBtn", "LowsAndFilesSaved"},
                {"SaveСommitteesBtn", "CommitteesSaved"},
                {"SaveСommitteesWorkBtn", "CommitteesWorkSaved"},
                {"SavePlenarySessionCalendarPlanBtn", "PlenarySessionSaved"},
                {"SaveDeputyBtn", "DeputySaved"},
                {"SaveDepVotingBtn", "DeputyVotingSaved"},
                {"SaveDepQueriesBtn", "DeputyQueriesSaved"},
                {"SaveDepSpeechesBtn", "DeputySpeechesSaved"},
                {"SaveDepLawActivityBtn", "DeputyLawActivitySaved"},
                {"SaveFractionsBtn", "FractionsSaved"}
            };

            ConvocationDates = new Dictionary<string, List<DateTime>>
            {
                { "VIII скликання", new List<DateTime> { new DateTime(2014, 11, 27), new DateTime(2017, 12, 12) } },
                { "VII скликання", new List<DateTime> { new DateTime(2012, 12, 12), new DateTime(2014, 10, 20) } },
                { "VI скликання", new List<DateTime> { new DateTime(2007, 11, 23), new DateTime(2012, 12, 06) } },
                { "V скликання", new List<DateTime> { new DateTime(2006, 05, 25), new DateTime(2007, 06, 19) } },
                { "IV скликання", new List<DateTime> { new DateTime(2002, 05, 14), new DateTime(2006, 04, 26) } }
            };
        }
    }
}
