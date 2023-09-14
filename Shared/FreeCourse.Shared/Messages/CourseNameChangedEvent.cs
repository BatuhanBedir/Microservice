namespace FreeCourse.Shared.Messages
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string UpdateName { get; set; }
    }
}
