using EduAppAPI.AppContext;
using EduAppAPI.Model;
using EduAppAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CourseController : Controller
    {
        private readonly TheDBContext _dbContext;
        public CourseController(TheDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                var Course = await _dbContext.Course
                    .FirstOrDefaultAsync(C => C.CourseId == id);

                if (Course == null)
                    return NotFound(new
                    {
                        message = $"Course with ID {id} was not found"
                    });

                return Ok(Course);
            }
            catch (Exception)
            {
                return BadRequest("Internal Server Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AllCourse()
        {
            var courses = await _dbContext.Course.ToListAsync();
            return Ok(courses);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUpdateCourse([FromBody] CourseCreateUpdateVModel postData)
        {

            if (postData.Id > 0)
            {
                var existingData = await _dbContext.Course.Where(cr => cr.CourseId == postData.Id).FirstOrDefaultAsync();
                if (existingData != null)
                {
                    existingData.Title = postData.Name;
                    existingData.CourseCode = postData.CourseCode;
                    _dbContext.Course.Update(existingData);
                    await _dbContext.SaveChangesAsync();
                    return Ok(existingData);
                }
                return BadRequest("No data found with the given ID");
            }
            bool courseExists = await _dbContext.Course
             .AnyAsync(cr => cr.Title == postData.Name);

            if (courseExists)
                return BadRequest("Course name already exists.");
            var newCourse = new Course
            {
                CourseCode = postData.CourseCode,
                Title = postData.Name
            };

            _dbContext.Course.Add(newCourse);
            await _dbContext.SaveChangesAsync();

            return Ok(newCourse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                _dbContext.Course.Where(cr => cr.CourseId == id).ExecuteDelete();
                await _dbContext.SaveChangesAsync();
                return Ok("delete success");

            }
            catch (Exception )
            {

                return BadRequest("Internal Server Error");
            }
        }
    }
}
