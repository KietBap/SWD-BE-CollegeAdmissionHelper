namespace CAH.ModelViews.AdmissionInforModelViews
{
    public class UpdateAdInforModelView
    {
        public DateTime Deadline { get; set; }
        public DateTime AdmisstionDate { get; set; }
        public int Quota { get; set; }
        public List<InforMethodModel> InforMethods { get; set; }
    }
}
