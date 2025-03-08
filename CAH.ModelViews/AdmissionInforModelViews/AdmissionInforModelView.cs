namespace CAH.ModelViews.AdmissionInforModelViews
{
    public class AdmissionInforModelView
    {
        public string Id { get; set; }
        public int Quota { get; set; }
        public DateTime AdmisstionDate { get; set; }
        public DateTime Deadline { get; set; }
        public List<ListInforMethodModelView> InforMethods { get; set; }
    }
}
