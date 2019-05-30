namespace GovernmentParse.Models
{
    public class SaveFilesErrorMessage : FileModel

    {
        public string SaveFilesErrorMsg { get; set; }

        public int? DocumentType { get; set; }

        public string UserMsg { get; set; }
    }
}
