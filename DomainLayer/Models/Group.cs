using DomainLayer.Common;

namespace DomainLayer.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public DateTime CreateDate { get; set; }
        public Teacher Teacher { get; set; }
    }
}
