using System.Text.Json.Serialization;

namespace CAH.ModelViews.AdmissionInforModelViews
{
    public class AdmissionInforModelView
    {
        public string UniMajorId { get; set; }
        public string AcademicYearId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int Quota { get; set; }
        public List<InforMethodModel> InforMethods { get; set; }
    }
}
