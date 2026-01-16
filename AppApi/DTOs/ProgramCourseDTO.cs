namespace EduAppAPI.DTOs
{
    public class ProgramCourseDTO
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public List<CourseStatusDTO>? CourseId { get; set; }
    }
}
