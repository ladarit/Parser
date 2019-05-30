namespace GovernmentParse.Models
{
    public struct RecordOptions
    {
        public string ElementName { get; set; }
        
        public string BlockName { get; set; }

        public string DefRowName { get; set; }

        public bool IsLiElement { get; set; }

        public bool IsMultiRecordInRow { get; set; }

        public bool IsChildBlock { get; set; }
    }
}
