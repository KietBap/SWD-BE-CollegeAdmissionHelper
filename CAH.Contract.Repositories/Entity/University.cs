using CAH.Core.Base;

namespace CAH.Contract.Repositories.Entity
{
	public class University : BaseEntity
	{
        public string Name { get; set; }
        public string Location { get; set; }
        public string? UniversityCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Accreditation { get; set; }
        public string Type { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int? RankingNational { get; set; }
        public int? RankingInternational { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<UniMajor> UniMajors { get; set; }
	}
}
