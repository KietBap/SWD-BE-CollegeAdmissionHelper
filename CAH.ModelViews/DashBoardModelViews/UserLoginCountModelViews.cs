using System;

namespace CAH.ModelViews.DashBoardModelViews
{
    public class UserLoginCountModelView 
    {
        public Guid UserId { get; set; } 
        public string UserName { get; set; } 
        public int LoginCount { get; set; } 
        public DateTimeOffset LoginTime { get; set; } 
    }
}
