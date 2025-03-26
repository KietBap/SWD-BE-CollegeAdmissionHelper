namespace CAH.ModelViews.SubScoreModelViews
{
    public class ListSubScoreModelView
    {
        public string Id { get; set; }
        public string SubjectName { get; set; }
        public DateTime Year { get; set; }
        public float Score { get; set; }
        public string ExamType { get; set; }
        public string? Class { get; set; }
    }
}
