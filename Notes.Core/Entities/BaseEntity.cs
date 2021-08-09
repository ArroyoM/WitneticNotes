using System;

namespace Notes.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity() { }
        public DateTime Created_time { get; set; }
        public DateTime Updated_time { get; set; }
        public DateTime? Deleted_time { get; set; }
    }
}
