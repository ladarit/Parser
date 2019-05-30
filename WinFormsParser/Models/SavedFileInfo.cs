namespace GovernmentParse.Models
{
    public class SavedFileInfo : BaseModel
    {
        public decimal NewCardId { get; set; }

        public decimal FileId { get; set; }

        public string FileName { get; set; }
    }
}
