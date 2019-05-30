//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GovernmentParse.Models
//{
//    public class Agenda
//    {
//        public string date { get; set; }
//        public string number { get; set; }
//        public string uri { get; set; }
//    }

//    public class RefBill
//    {
//        public int id { get; set; }
//    }

//    public class Alternative
//    {
//        public List<RefBill> refBills { get; set; }
//    }

//    public class RefBills
//    {
//        public int id { get; set; }
//    }

//    public class Bind
//    {
//        public RefBills refBills { get; set; }
//    }

//    public class CurrentPhase
//    {
//        public string date { get; set; }
//        public string title { get; set; }
//    }

//    public class Source
//    {
//        public object document { get; set; }
//    }

//    public class Document
//    {
//        public string date { get; set; }
//        public string type { get; set; }
//        public string uri { get; set; }
//    }

//    public class Workflow
//    {
//        public List<Document> document { get; set; }
//    }

//    public class Documents
//    {
//        public Source source { get; set; }
//        public Workflow workflow { get; set; }
//    }

//    public class Person
//    {
//        public int id { get; set; }
//        public string firstname { get; set; }
//        public string patronymic { get; set; }
//        public string surname { get; set; }
//    }

//    public class Executive
//    {
//        public string department { get; set; }
//        public Person person { get; set; }
//    }

//    public class Person2
//    {
//        public int id { get; set; }
//        public string firstname { get; set; }
//        public string patronymic { get; set; }
//        public string surname { get; set; }
//    }

//    public class Executive2
//    {
//        public string department { get; set; }
//        public Person2 person { get; set; }
//    }

//    public class MainExecutives
//    {
//        public Executive2 executive { get; set; }
//    }

//    public class Passing
//    {
//        public string date { get; set; }
//        public string title { get; set; }
//    }

//    public class WorkOuts
//    {
//        public string date { get; set; }
//        public string documentType { get; set; }
//        public object workOutCommittees { get; set; }
//    }

//    public class JsonLawModel
//    {
//        //public int id { get; set; }
//        //public Agenda agenda { get; set; }
//        //public Alternative alternative { get; set; }
//        //public object authors { get; set; }
//        //public Bind bind { get; set; }
//        //public CurrentPhase currentPhase { get; set; }
//        //public Documents documents { get; set; }
//        //public List<Executive> executives { get; set; }
//        //public object initiators { get; set; }
//        //public MainExecutives mainExecutives { get; set; }
//        public string number { get; set; }
//        //public List<Passing> passings { get; set; }
//        //public string registrationConvocation { get; set; }
//        public string registrationDate { get; set; }
//        //public string registrationSession { get; set; }
//        //public string rubric { get; set; }
//        //public string subject { get; set; }
//        //public string title { get; set; }
//        public string type { get; set; }
//        public string uri { get; set; }
//        //public WorkOuts workOuts { get; set; }
//    }

//    //public class JsonLawModel
//    //{
//    //    public int id { get; set; }
//    //    public Agenda agenda { get; set; }
//    //    //public Alternative alternative { get; set; }
//    //    //public object authors { get; set; }
//    //    //public Bind bind { get; set; }
//    //    //public CurrentPhase currentPhase { get; set; }
//    //    public Documents documents { get; set; }
//    //    //public IList<Executive> executives { get; set; }
//    //    //public IList<Initiator> initiators { get; set; }
//    //    //public MainExecutives mainExecutives { get; set; }
//    //    //public string number { get; set; }
//    //    //public IList<Passing> passings { get; set; }
//    //    //public string registrationConvocation { get; set; }
//    //    //public string registrationDate { get; set; }
//    //    //public string registrationSession { get; set; }
//    //    //public string rubric { get; set; }
//    //    //public string subject { get; set; }
//    //    //public string title { get; set; }
//    //    //public string type { get; set; }
//    //    //public string uri { get; set; }
//    //    //public WorkOuts workOuts { get; set; }
//    //}

//    //public class Agenda
//    //{
//    //    public string date { get; set; }
//    //    public string number { get; set; }
//    //    public string uri { get; set; }
//    //}

//    //public class RefBill
//    //{
//    //    public int? id { get; set; }
//    //}

//    //public class Alternative
//    //{
//    //    public List<RefBill> refBills { get; set; }
//    //}

//    //public class RefBills
//    //{
//    //    public int? id { get; set; }
//    //}

//    //public class Bind
//    //{
//    //    public RefBills refBills { get; set; }
//    //}

//    //public class CurrentPhase
//    //{
//    //    public string date { get; set; }
//    //    public string title { get; set; }
//    //}

//    //public class Document
//    //{
//    //    public string date { get; set; }
//    //    public string type { get; set; }
//    //    public string uri { get; set; }
//    //}

//    //public class Source
//    //{
//    //    public IEnumerable<Document> document { get; set; }
//    //}


//    //public class Workflow
//    //{
//    //    public IList<Document> document { get; set; }
//    //}

//    //public class Documents
//    //{
//    //    public Source source { get; set; }
//    //    public Workflow workflow { get; set; }
//    //}

//    //public class Person
//    //{
//    //    public int id { get; set; }
//    //    public string firstname { get; set; }
//    //    public string patronymic { get; set; }
//    //    public string surname { get; set; }
//    //}

//    //public class Executive
//    //{
//    //    public string department { get; set; }
//    //    public Person person { get; set; }
//    //}

//    //public class Official
//    //{
//    //    public string convocation { get; set; }
//    //    public object department { get; set; }
//    //    public Person person { get; set; }
//    //    public object post { get; set; }
//    //}

//    //public class Initiator
//    //{
//    //    public Official official { get; set; }
//    //}

//    //public class MainExecutives
//    //{
//    //    public Executive executive { get; set; }
//    //}

//    //public class Passing
//    //{
//    //    public string date { get; set; }
//    //    public string title { get; set; }
//    //}

//    //public class WorkOutCommittee
//    //{
//    //    public string dateGot { get; set; }
//    //    public string datePassed { get; set; }
//    //    public string title { get; set; }
//    //}

//    //public class WorkOuts
//    //{
//    //    public string date { get; set; }
//    //    public string documentType { get; set; }
//    //    public IList<WorkOutCommittee> workOutCommittees { get; set; }
//    //}
//}
