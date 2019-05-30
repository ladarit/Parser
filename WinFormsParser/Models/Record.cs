using System;

namespace GovernmentParse.Models
{
    public class Record<T> : BaseModel, ICloneable
    {
        public string Name { get; set; }

        public T Value { get; set; }

        public string GeneralSign { get; set; }

        public string ParentBlockGeneralSign { get; set; }

        public string Id { get; set; }

        public Record<T> Clone() //todo переписать
        {
            return (Record<T>)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return ((ICloneable)this).Clone();
        }
    }
}
