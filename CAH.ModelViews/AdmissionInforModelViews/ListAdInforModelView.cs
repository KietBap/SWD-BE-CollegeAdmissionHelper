namespace CAH.ModelViews.AdmissionInforModelViews
{
    public class ListAdInforModelView
    {
        public string Id { get; set; }
        public string UniversityName { get; set; }
        public string MajorName { get; set; }
        public DateTime AdmisstionDate { get; set; }
        public DateTime Deadline { get; set; }
        public int Quota { get; set; }
    }
}
