namespace CAH.ModelViews.UniversityModelViews
{
	public class UniversityMV
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string UniversityCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly EstablishedDate { get; set; }
        public string? Accreditation { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int? RankingNational { get; set; }
        public int? RankingInternational { get; set; }

    }
}
