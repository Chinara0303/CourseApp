using DomainLayer.Common;

namespace DomainLayer.Models
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }

    }
}
