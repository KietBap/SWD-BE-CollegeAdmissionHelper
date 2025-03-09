namespace CAH.ModelViews.AdmissionInforModelViews
{
    public class CreateAdInforModelView
    {
        public string UniMajorId { get; set; }
        public string AcademicYearId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime AdmisstionDate { get; set; }
        public int Quota { get; set; }
        public List<InforMethodModel> InforMethods { get; set; }
    }
}
