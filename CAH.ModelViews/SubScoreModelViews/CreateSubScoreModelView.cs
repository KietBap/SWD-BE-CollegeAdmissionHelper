namespace CAH.ModelViews.SubScoreModelViews
{
    public class CreateSubScoreModelView
    {
        public string SubjectId { get; set; }
        public string UserId { get; set; }
        public DateTime Year { get; set; }
        public float Score { get; set; }
        public string ExamType { get; set; }
        public string? Class { get; set; }
    }
}
