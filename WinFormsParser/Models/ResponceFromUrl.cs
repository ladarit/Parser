namespace GovernmentParse.Models
{
    public class ResponceFromUrl<T> : BaseModel
    {
        public T ReceivedData { get; set; }

        public string FileType { get; set; }
    }
}
