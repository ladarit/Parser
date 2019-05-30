//using System;
//using System.Collections.Generic;
//using System.Linq;
//using GovernmentParse.Helpers;
//using log4net.Core;
//using Newtonsoft.Json;

//namespace GovernmentParse.Services
//{
//    public partial class JsonlawModel : IEquatable<JsonlawModel>
//    {
//        [JsonProperty("id")]
//        public long Id { get; set; }

//        [JsonProperty("registrationSession")]
//        public string RegistrationSession { get; set; }

//        [JsonProperty("rubric")]
//        public string Rubric { get; set; }

//        [JsonProperty("subject")]
//        public string Subject { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }

//        [JsonProperty("uri")]
//        public string Uri { get; set; }

//        [JsonProperty("redaction")]
//        public string Redaction { get; set; }

//        [JsonProperty("registrationConvocation")]
//        public string RegistrationConvocation { get; set; }

//        [JsonProperty("isUrgent")]
//        public bool? IsUrgent { get; set; }

//        [JsonProperty("agenda")]
//        public Acts Agenda { get; set; }

//        [JsonProperty("alternative")]
//        public Alternative Alternative { get; set; }

//        [JsonProperty("authors")]
//        public AuthorsUnion Authors { get; set; }

//        [JsonProperty("bind")]
//        public Alternative Bind { get; set; }

//        [JsonProperty("currentPhase")]
//        public CurrentPhase CurrentPhase { get; set; }

//        [JsonProperty("documents")]
//        public Documents Documents { get; set; }

//        [JsonProperty("executives")]
//        public Executives Executives { get; set; }

//        [JsonProperty("initiators")]
//        public Initiators Initiators { get; set; }

//        [JsonProperty("mainExecutives")]
//        public MainExecutives MainExecutives { get; set; }

//        [JsonProperty("number")]
//        public Number Number { get; set; }

//        [JsonProperty("passings")]
//        public List<Passing> Passings { get; set; }

//        [JsonProperty("registrationDate")]
//        public DateTime RegistrationDate { get; set; }

//        [JsonProperty("workOuts")]
//        public WorkOuts WorkOuts { get; set; }

//        [JsonProperty("acts")]
//        public Acts Acts { get; set; }

//        public override bool Equals(object obj)
//        {
//            return this.Equals(obj as JsonlawModel);
//        }

//        public bool Equals(JsonlawModel comparedObj)
//        {
//            try
//            {
//                return comparedObj != null
//                       && Id.Equals(comparedObj.Id)
//                       && string.Equals(RegistrationConvocation, comparedObj.RegistrationConvocation)
//                       && string.Equals(Rubric, comparedObj.Rubric) && string.Equals(Subject, comparedObj.Subject)
//                       && string.Equals(Title, comparedObj.Title) && string.Equals(Type, comparedObj.Type)
//                       && string.Equals(Uri, comparedObj.Uri) && string.Equals(Redaction, comparedObj.Redaction)
//                       && (IsUrgent?.Equals(comparedObj.IsUrgent)?? comparedObj.IsUrgent==null)
//                       && (Agenda?.Equals(comparedObj.Agenda)?? comparedObj.Agenda == null)
//                       && (Alternative?.Equals(comparedObj.Alternative)??comparedObj.Alternative==null)
//                       && Authors.Equals(comparedObj.Authors)
//                       && (Bind?.Equals(comparedObj.Bind) ?? comparedObj.Bind == null)
//                       && (CurrentPhase?.Equals(comparedObj.CurrentPhase) ?? comparedObj.CurrentPhase == null)
//                       && (Documents?.Equals(comparedObj.Documents) ?? comparedObj.Documents ==null)
//                       && Executives.Equals(comparedObj.Executives)
//                       && Initiators.Equals(comparedObj.Initiators)
//                       && (MainExecutives?.Equals(comparedObj.MainExecutives) ?? comparedObj.MainExecutives == null)
//                       && Number.Equals(comparedObj.Number)
//                       && (Passings?.ListEqualsTo(comparedObj.Passings) ?? comparedObj.Passings == null)
//                       && DateTime.Equals(RegistrationDate, comparedObj.RegistrationDate)
//                       && WorkOuts.Equals(comparedObj.WorkOuts)
//                       && (Acts?.Equals(comparedObj.Acts) ?? comparedObj.Acts == null);

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }
//        }
//    }

//    public partial class Acts : IEquatable<Acts>
//    {
//        [JsonProperty("date")]
//        public DateTime Date { get; set; }

//        [JsonProperty("number")]
//        public string Number { get; set; }

//        [JsonProperty("uri")]
//        public string Uri { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Acts);
//        }

//        public bool Equals(Acts comparedObj)
//        {
//            return comparedObj != null && string.Equals(Number, comparedObj.Number) && string.Equals(Uri, comparedObj.Uri) && DateTime.Equals(Date, comparedObj.Date);
//        }
//    }

//    public partial class Alternative : IEquatable<Alternative>
//    {
//        [JsonProperty("refBills")]
//        public RefBills RefBills { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Alternative);
//        }

//        public bool Equals(Alternative comparedObj)
//        {
//            return comparedObj != null && RefBills.Equals(comparedObj.RefBills);
//        }
//    }

//    public partial class RefBill : IEquatable<RefBill>
//    {
//        [JsonProperty("id")]
//        public long Id { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as RefBill);
//        }

//        public bool Equals(RefBill comparedObj)
//        {
//            return comparedObj != null && long.Equals(Id, comparedObj.Id);
//        }
//    }

//    public partial class Author : IEquatable<Author>
//    {
//        [JsonProperty("official")]
//        public AuthorOfficial Official { get; set; }

//        [JsonProperty("outer")]
//        public AuthorOuter Outer { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Author);
//        }

//        public bool Equals(Author comparedObj)
//        {
//            return comparedObj != null && (Official?.Equals(comparedObj.Official)??comparedObj.Official==null) && (Outer?.Equals(comparedObj.Outer)?? comparedObj.Outer == null);
//        }
//    }

//    public partial class AuthorOfficial : IEquatable<AuthorOfficial>
//    {
//        [JsonProperty("convocation")]
//        public string Convocation { get; set; }

//        [JsonProperty("department")]
//        public string Department { get; set; }

//        [JsonProperty("person")]
//        public Person Person { get; set; }

//        [JsonProperty("post")]
//        public string Post { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as AuthorOfficial);
//        }

//        public bool Equals(AuthorOfficial comparedObj)
//        {
//            return comparedObj != null && string.Equals(Convocation, comparedObj.Convocation) 
//                                       && string.Equals(Department, comparedObj.Department)
//                                       && string.Equals(Post, comparedObj.Post)
//                                       && (Person?.Equals(comparedObj.Person)??comparedObj.Person==null);
//        }
//    }

//    public partial class Person : IEquatable<Person>
//    {
//        [JsonProperty("id")]
//        public Id Id { get; set; }

//        [JsonProperty("firstname")]
//        public string Firstname { get; set; }

//        [JsonProperty("patronymic")]
//        public string Patronymic { get; set; }

//        [JsonProperty("surname")]
//        public string Surname { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Person);
//        }

//        public bool Equals(Person comparedObj)
//        {
//            return comparedObj != null && Id.Equals(comparedObj.Id) && string.Equals(Firstname, comparedObj.Firstname)
//                                            && string.Equals(Patronymic, comparedObj.Patronymic)
//                                            && string.Equals(Surname, comparedObj.Surname);
//        }
//    }

//    public partial class AuthorOuter : IEquatable<AuthorOuter>
//    {
//        [JsonProperty("organization")]
//        public string Organization { get; set; }

//        [JsonProperty("person")]
//        public Person Person { get; set; }

//        [JsonProperty("post")]
//        public string Post { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as AuthorOuter);
//        }

//        public bool Equals(AuthorOuter comparedObj)
//        {
//            return comparedObj != null && string.Equals(Organization, comparedObj.Organization) 
//                                       && (Person?.Equals(comparedObj.Person)??comparedObj.Person==null)
//                                       && string.Equals(Post, comparedObj.Post);
//        }
//    }

//    public partial class AuthorsClass : IEquatable<AuthorsClass>
//    {
//        [JsonProperty("official")]
//        public AuthorsOfficial Official { get; set; }

//        [JsonProperty("outer")]
//        public AuthorsOuter Outer { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as AuthorsClass);
//        }

//        public bool Equals(AuthorsClass comparedObj)
//        {
//            return comparedObj != null && (Official?.Equals(comparedObj.Official)??comparedObj.Official==null) 
//                                       && (Outer?.Equals(comparedObj.Outer)??comparedObj.Outer==null);
//        }
//    }

//    public partial class AuthorsOfficial : IEquatable<AuthorsOfficial>
//    {
//        [JsonProperty("convocation")]
//        public string Convocation { get; set; }

//        [JsonProperty("department")]
//        public object Department { get; set; }

//        [JsonProperty("person")]
//        public Person Person { get; set; }

//        [JsonProperty("post")]
//        public object Post { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as AuthorsOfficial);
//        }

//        public bool Equals(AuthorsOfficial comparedObj)
//        {
//            return comparedObj != null && string.Equals(Convocation, comparedObj.Convocation) && (Department?.Equals(comparedObj.Department)?? comparedObj.Department==null)
//                                        && (Person?.Equals(comparedObj.Person)?? comparedObj.Person == null) && (Post?.Equals(comparedObj.Post)?? comparedObj.Post == null);
//        }
//    }

//    public partial class AuthorsOuter : IEquatable<AuthorsOuter>
//    {
//        [JsonProperty("organization")]
//        public string Organization { get; set; }

//        [JsonProperty("person")]
//        public Person Person { get; set; }

//        [JsonProperty("post")]
//        public string Post { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as AuthorsOuter);
//        }

//        public bool Equals(AuthorsOuter comparedObj)
//        {
//            return comparedObj != null && string.Equals(Organization ,comparedObj.Organization) && (Person?.Equals(comparedObj.Person)?? comparedObj.Person == null)
//                                       && string.Equals(Post, comparedObj.Post);
//        }
//    }

//    public partial class CurrentPhase : IEquatable<CurrentPhase>
//    {
//        [JsonProperty("date")]
//        public string Date { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as CurrentPhase);
//        }

//        public bool Equals(CurrentPhase comparedObj)
//        {
//            return comparedObj != null && string.Equals(Date, comparedObj.Date) && string.Equals(Title, comparedObj.Title);
//        }
//    }

//    public partial class Documents : IEquatable<Documents>
//    {
//        [JsonProperty("source")]
//        public Source Source { get; set; }

//        [JsonProperty("workflow")]
//        public Source Workflow { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Documents);
//        }

//        public bool Equals(Documents comparedObj)
//        {
//            return comparedObj != null && (Source?.Equals(comparedObj.Source)?? comparedObj.Source == null) && (Workflow?.Equals(comparedObj.Workflow)?? comparedObj.Workflow == null);
//        }
//    }

//    public partial class Source : IEquatable<Source>
//    {
//        [JsonProperty("document")]
//        public DocumentUnion Document { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Source);
//        }

//        public bool Equals(Source comparedObj)
//        {
//            return comparedObj != null && Document.Equals(comparedObj.Document);
//        }
//    }

//    public partial class DocumentClass : IEquatable<DocumentClass>
//    {
//        [JsonProperty("date")]
//        public DateTime? Date { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }

//        [JsonProperty("uri")]
//        public string Uri { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as DocumentClass);
//        }

//        public bool Equals(DocumentClass comparedObj)
//        {
//            return comparedObj != null && DateTime.Equals(Date, comparedObj.Date) && string.Equals(Type, comparedObj.Type)
//                   && string.Equals(Uri, comparedObj.Uri) && string.Equals(Title, comparedObj.Title);
//        }
//    }

//    public partial class WelcomeExecutive : IEquatable<WelcomeExecutive>
//    {
//        [JsonProperty("department")]
//        public string Department { get; set; }

//        [JsonProperty("person")]
//        public Person Person { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as WelcomeExecutive);
//        }

//        public bool Equals(WelcomeExecutive comparedObj)
//        {
//            return comparedObj != null && string.Equals(Department, comparedObj.Department) && (Person?.Equals(comparedObj.Person)?? comparedObj.Person == null);
//        }
//    }

//    public partial class Initiator : IEquatable<Initiator>
//    {
//        [JsonProperty("official")]
//        public AuthorOfficial Official { get; set; }

//        [JsonProperty("outer")]
//        public AuthorsOuter Outer { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Initiator);
//        }

//        public bool Equals(Initiator comparedObj)
//        {
//            return comparedObj != null && (Official?.Equals(comparedObj.Official)?? comparedObj.Official == null) && (Outer?.Equals(comparedObj.Outer)?? comparedObj.Outer == null);
//        }
//    }

//    public partial class MainExecutives : IEquatable<MainExecutives>
//    {
//        [JsonProperty("executive")]
//        public MainExecutivesExecutive Executive { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as MainExecutives);
//        }

//        public bool Equals(MainExecutives comparedObj)
//        {
//            return comparedObj != null && (Executive?.Equals(comparedObj.Executive) ?? comparedObj.Executive == null);
//        }
//    }

//    public partial class MainExecutivesExecutive : IEquatable<MainExecutivesExecutive>
//    {
//        [JsonProperty("department")]
//        public string Department { get; set; }

//        [JsonProperty("person")]
//        public Person Person { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as MainExecutivesExecutive);
//        }

//        public bool Equals(MainExecutivesExecutive comparedObj)
//        {
//            return comparedObj != null && string.Equals(Department ,comparedObj.Department) && (Person?.Equals(comparedObj.Person) ?? comparedObj.Person == null);
//        }
//    }

//    public partial class Passing : IEquatable<Passing>
//    {
//        [JsonProperty("date")]
//        public DateTime Date { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as Passing);
//        }

//        public bool Equals(Passing comparedObj)
//        {
//            return comparedObj != null && DateTime.Equals(Date, comparedObj.Date) && string.Equals(Title, comparedObj.Title);
//        }
//    }

//    public partial class WorkOut : IEquatable<WorkOut>
//    {
//        [JsonProperty("date")]
//        public DateTime Date { get; set; }

//        [JsonProperty("documentType")]
//        public string DocumentType { get; set; }

//        [JsonProperty("workOutCommittees")]
//        public WorkOutCommittees WorkOutCommittees { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as WorkOut);
//        }

//        public bool Equals(WorkOut comparedObj)
//        {
//            return comparedObj != null && DateTime.Equals(Date, comparedObj.Date) && string.Equals(DocumentType, comparedObj.DocumentType) && WorkOutCommittees.Equals(comparedObj.WorkOutCommittees);
//        }
//    }

//    public partial class WorkOutCommittee : IEquatable<WorkOutCommittee>
//    {
//        [JsonProperty("dateGot")]
//        public DateTime? DateGot { get; set; }

//        [JsonProperty("datePassed")]
//        public DateTime? DatePassed { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as WorkOutCommittee);
//        }

//        public bool Equals(WorkOutCommittee comparedObj)
//        {
//            return comparedObj != null && DateTime.Equals(DateGot, comparedObj.DateGot) && DateTime.Equals(DatePassed, comparedObj.DatePassed) && string.Equals(Title, comparedObj.Title);
//        }
//    }

//    public partial struct RefBills : IEquatable<RefBills>
//    {
//        public RefBill RefBill;
//        public List<RefBill> RefBillArray;

//        public override bool Equals(object obj)
//        {
//            return obj is RefBills && Equals((RefBills)obj);
//        }

//        public bool Equals(RefBills comparedObj)
//        {
//            return (RefBill?.Equals(comparedObj.RefBill) ?? comparedObj.RefBill == null) && (RefBillArray?.ListEqualsTo(comparedObj.RefBillArray) ?? comparedObj.RefBillArray == null);
//        }
//    }

//    public partial struct AuthorsUnion : IEquatable<AuthorsUnion>
//    {
//        public List<Author> AuthorArray;
//        public AuthorsClass AuthorsClass;

//        public override bool Equals(object obj)
//        {
//            return obj is AuthorsUnion && Equals((AuthorsUnion) obj);
//        }

//        public bool Equals(AuthorsUnion comparedObj)
//        {
//            return (AuthorArray?.ListEqualsTo(comparedObj.AuthorArray) ?? comparedObj.AuthorArray == null) && (AuthorsClass?.Equals(comparedObj.AuthorsClass)?? comparedObj.AuthorsClass == null);
//        }
//    }

//    public partial struct Id : IEquatable<Id>
//    {
//        public long? Integer;
//        public string String;

//        public override bool Equals(object obj)
//        {
//            return obj is Id && Equals((Id)obj);
//        }

//        public bool Equals(Id comparedObj)
//        {
//            return long.Equals(Integer, comparedObj.Integer) && string.Equals(String, comparedObj.String);
//        }
//    }

//    public partial struct DocumentUnion : IEquatable<DocumentUnion>
//    {
//        public DocumentClass DocumentClass;
//        public List<DocumentClass> DocumentClassArray;

//        public override bool Equals(object obj)
//        {
//            return obj is DocumentUnion && Equals((DocumentUnion)obj);
//        }

//        public bool Equals(DocumentUnion comparedObj)
//        {
//            return (DocumentClass?.Equals(comparedObj.DocumentClass) ?? comparedObj.DocumentClass == null) && (DocumentClassArray?.ListEqualsTo(comparedObj.DocumentClassArray)?? comparedObj.DocumentClassArray == null);
//        }
//    }

//    public partial struct Executives : IEquatable<Executives>
//    {
//        public WelcomeExecutive WelcomeExecutive;
//        public List<WelcomeExecutive> WelcomeExecutiveArray;

//        public override bool Equals(object obj)
//        {
//            return obj is Executives && Equals((Executives)obj);
//        }

//        public bool Equals(Executives comparedObj)
//        {
//            return (WelcomeExecutive?.Equals(comparedObj.WelcomeExecutive)?? comparedObj.WelcomeExecutive == null) && (WelcomeExecutiveArray?.ListEqualsTo(comparedObj.WelcomeExecutiveArray)?? comparedObj.WelcomeExecutiveArray == null);
//        }
//    }

//    public partial struct Initiators : IEquatable<Initiators>
//    {
//        public Initiator Initiator;
//        public List<Initiator> InitiatorArray;

//        public override bool Equals(object obj)
//        {
//            return obj is Initiators && Equals((Initiators)obj);
//        }

//        public bool Equals(Initiators comparedObj)
//        {
//            return (Initiator?.Equals(comparedObj.Initiator)?? comparedObj.Initiator == null) && (InitiatorArray?.ListEqualsTo(comparedObj.InitiatorArray)?? comparedObj.InitiatorArray == null);
//        }
//    }

//    public partial struct Number : IEquatable<Number>
//    {
//        public long? Integer;
//        public string String;

//        public override bool Equals(object obj)
//        {
//            return obj is Number && Equals((Number)obj);
//        }

//        public bool Equals(Number comparedObj)
//        {
//            return long.Equals(Integer, comparedObj.Integer) && string.Equals(String, comparedObj.String);
//        }
//    }

//    public partial struct WorkOuts : IEquatable<WorkOuts>
//    {
//        public WorkOut WorkOut;
//        public List<WorkOut> WorkOutArray;

//        public override bool Equals(object obj)
//        {
//            return obj is WorkOuts && Equals((WorkOuts)obj);
//        }

//        public bool Equals(WorkOuts comparedObj)
//        {
//            return (WorkOut?.Equals(comparedObj.WorkOut) ?? comparedObj.WorkOut == null) && (WorkOutArray?.ListEqualsTo(comparedObj.WorkOutArray) ?? comparedObj.WorkOutArray == null);
//        }
//    }

//    public partial struct WorkOutCommittees : IEquatable<WorkOutCommittees>
//    {
//        public WorkOutCommittee WorkOutCommittee;
//        public List<WorkOutCommittee> WorkOutCommitteeArray;

//        public override bool Equals(object obj)
//        {
//            return obj is WorkOutCommittees && Equals((WorkOutCommittees)obj);
//        }

//        public bool Equals(WorkOutCommittees comparedObj)
//        {
//            return (WorkOutCommittee?.Equals(comparedObj.WorkOutCommittee)?? comparedObj.WorkOutCommittee == null) && (WorkOutCommitteeArray?.ListEqualsTo(comparedObj.WorkOutCommitteeArray) ?? comparedObj.WorkOutCommitteeArray == null);
//        }
//    }

//    public partial class JsonlawModel
//    {
//        public static List<JsonlawModel> FromJson(string json) => JsonConvert.DeserializeObject<List<JsonlawModel>>(json, CustomJsonConverter.Settings);
//    }

//    public partial struct RefBills
//    {
//        public RefBills(JsonReader reader, JsonSerializer serializer)
//        {
//            RefBill = null;
//            RefBillArray = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.StartArray:
//                    RefBillArray = serializer.Deserialize<List<RefBill>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    RefBill = serializer.Deserialize<RefBill>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert RefBills");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (RefBill != null)
//            {
//                serializer.Serialize(writer, RefBill);
//                return;
//            }
//            if (RefBillArray != null)
//            {
//                serializer.Serialize(writer, RefBillArray);
//                return;
//            }
//            throw new Exception("Union must not be null");
//        }
//    }

//    public partial struct AuthorsUnion
//    {
//        public AuthorsUnion(JsonReader reader, JsonSerializer serializer)
//        {
//            AuthorArray = null;
//            AuthorsClass = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.Null:
//                    return;
//                case JsonToken.StartArray:
//                    AuthorArray = serializer.Deserialize<List<Author>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    AuthorsClass = serializer.Deserialize<AuthorsClass>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert AuthorsUnion");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (AuthorArray != null)
//            {
//                serializer.Serialize(writer, AuthorArray);
//                return;
//            }
//            if (AuthorsClass != null)
//            {
//                serializer.Serialize(writer, AuthorsClass);
//                return;
//            }
//            writer.WriteNull();
//        }
//    }

//    public partial struct Id
//    {
//        public Id(JsonReader reader, JsonSerializer serializer)
//        {
//            Integer = null;
//            String = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.Null:
//                    return;
//                case JsonToken.Integer:
//                    Integer = serializer.Deserialize<long>(reader);
//                    return;
//                case JsonToken.String:
//                case JsonToken.Date:
//                    String = serializer.Deserialize<string>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert Id");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (Integer != null)
//            {
//                serializer.Serialize(writer, Integer);
//                return;
//            }
//            if (String != null)
//            {
//                serializer.Serialize(writer, String);
//                return;
//            }
//            writer.WriteNull();
//        }
//    }

//    public partial struct DocumentUnion
//    {
//        public DocumentUnion(JsonReader reader, JsonSerializer serializer)
//        {
//            DocumentClass = null;
//            DocumentClassArray = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.StartArray:
//                    DocumentClassArray = serializer.Deserialize<List<DocumentClass>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    DocumentClass = serializer.Deserialize<DocumentClass>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert DocumentUnion");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (DocumentClass != null)
//            {
//                serializer.Serialize(writer, DocumentClass);
//                return;
//            }
//            if (DocumentClassArray != null)
//            {
//                serializer.Serialize(writer, DocumentClassArray);
//                return;
//            }
//            throw new Exception("Union must not be null");
//        }
//    }

//    public partial struct Executives
//    {
//        public Executives(JsonReader reader, JsonSerializer serializer)
//        {
//            WelcomeExecutive = null;
//            WelcomeExecutiveArray = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.Null:
//                    return;
//                case JsonToken.StartArray:
//                    WelcomeExecutiveArray = serializer.Deserialize<List<WelcomeExecutive>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    WelcomeExecutive = serializer.Deserialize<WelcomeExecutive>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert Executives");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (WelcomeExecutive != null)
//            {
//                serializer.Serialize(writer, WelcomeExecutive);
//                return;
//            }
//            if (WelcomeExecutiveArray != null)
//            {
//                serializer.Serialize(writer, WelcomeExecutiveArray);
//                return;
//            }
//            writer.WriteNull();
//        }
//    }

//    public partial struct Initiators
//    {
//        public Initiators(JsonReader reader, JsonSerializer serializer)
//        {
//            Initiator = null;
//            InitiatorArray = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.StartArray:
//                    InitiatorArray = serializer.Deserialize<List<Initiator>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    Initiator = serializer.Deserialize<Initiator>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert Initiators");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (Initiator != null)
//            {
//                serializer.Serialize(writer, Initiator);
//                return;
//            }
//            if (InitiatorArray != null)
//            {
//                serializer.Serialize(writer, InitiatorArray);
//                return;
//            }
//            throw new Exception("Union must not be null");
//        }
//    }

//    public partial struct Number
//    {
//        public Number(JsonReader reader, JsonSerializer serializer)
//        {
//            Integer = null;
//            String = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.Integer:
//                    Integer = serializer.Deserialize<long>(reader);
//                    return;
//                case JsonToken.String:
//                case JsonToken.Date:
//                    String = serializer.Deserialize<string>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert Number");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (Integer != null)
//            {
//                serializer.Serialize(writer, Integer);
//                return;
//            }
//            if (String != null)
//            {
//                serializer.Serialize(writer, String);
//                return;
//            }
//            throw new Exception("Union must not be null");
//        }
//    }

//    public partial struct WorkOuts
//    {
//        public WorkOuts(JsonReader reader, JsonSerializer serializer)
//        {
//            WorkOut = null;
//            WorkOutArray = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.Null:
//                    return;
//                case JsonToken.StartArray:
//                    WorkOutArray = serializer.Deserialize<List<WorkOut>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    WorkOut = serializer.Deserialize<WorkOut>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert WorkOuts");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (WorkOut != null)
//            {
//                serializer.Serialize(writer, WorkOut);
//                return;
//            }
//            if (WorkOutArray != null)
//            {
//                serializer.Serialize(writer, WorkOutArray);
//                return;
//            }
//            writer.WriteNull();
//        }
//    }

//    public partial struct WorkOutCommittees
//    {
//        public WorkOutCommittees(JsonReader reader, JsonSerializer serializer)
//        {
//            WorkOutCommittee = null;
//            WorkOutCommitteeArray = null;

//            switch (reader.TokenType)
//            {
//                case JsonToken.StartArray:
//                    WorkOutCommitteeArray = serializer.Deserialize<List<WorkOutCommittee>>(reader);
//                    return;
//                case JsonToken.StartObject:
//                    WorkOutCommittee = serializer.Deserialize<WorkOutCommittee>(reader);
//                    return;
//            }
//            throw new Exception("Cannot convert WorkOutCommittees");
//        }

//        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
//        {
//            if (WorkOutCommittee != null)
//            {
//                serializer.Serialize(writer, WorkOutCommittee);
//                return;
//            }
//            if (WorkOutCommitteeArray != null)
//            {
//                serializer.Serialize(writer, WorkOutCommitteeArray);
//                return;
//            }
//            throw new Exception("Union must not be null");
//        }
//    }

//    public static class Serialize
//    {
//        public static string ToJson(this List<JsonlawModel> self) => JsonConvert.SerializeObject((object) self, (JsonSerializerSettings)CustomJsonConverter.Settings);
//    }

//    public class CustomJsonConverter : JsonConverter
//    {
//        public override bool CanConvert(Type t) => t == typeof(RefBills) || t == typeof(AuthorsUnion) || t == typeof(Id) || t == typeof(DocumentUnion) || t == typeof(Executives) || t == typeof(Initiators) || t == typeof(Number) || t == typeof(WorkOuts) || t == typeof(WorkOutCommittees);

//        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//        {
//            if (t == typeof(RefBills))
//                return new RefBills(reader, serializer);
//            if (t == typeof(AuthorsUnion))
//                return new AuthorsUnion(reader, serializer);
//            if (t == typeof(Id))
//                return new Id(reader, serializer);
//            if (t == typeof(DocumentUnion))
//                return new DocumentUnion(reader, serializer);
//            if (t == typeof(Executives))
//                return new Executives(reader, serializer);
//            if (t == typeof(Initiators))
//                return new Initiators(reader, serializer);
//            if (t == typeof(Number))
//                return new Number(reader, serializer);
//            if (t == typeof(WorkOuts))
//                return new WorkOuts(reader, serializer);
//            if (t == typeof(WorkOutCommittees))
//                return new WorkOutCommittees(reader, serializer);
//            throw new Exception("Unknown type");
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            var t = value.GetType();
//            if (t == typeof(RefBills))
//            {
//                ((RefBills)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(AuthorsUnion))
//            {
//                ((AuthorsUnion)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(Id))
//            {
//                ((Id)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(DocumentUnion))
//            {
//                ((DocumentUnion)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(Executives))
//            {
//                ((Executives)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(Initiators))
//            {
//                ((Initiators)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(Number))
//            {
//                ((Number)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(WorkOuts))
//            {
//                ((WorkOuts)value).WriteJson(writer, serializer);
//                return;
//            }
//            if (t == typeof(WorkOutCommittees))
//            {
//                ((WorkOutCommittees)value).WriteJson(writer, serializer);
//                return;
//            }
//            throw new Exception("Unknown type");
//        }

//        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//        {
//            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//            DateParseHandling = DateParseHandling.None,
//            Converters = { new CustomJsonConverter() }
//        };
//    }
//}
