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
                var existingData = await _dbContext.Course.Where(cr => cr.ID == postData.Id).FirstOrDefaultAsync();
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
                _dbContext.Course.Where(cr=> cr.ID == id).ExecuteDelete();
                await _dbContext.SaveChangesAsync();
                return Ok("delete success");

            }
            catch (Exception ex)
            {

                return BadRequest("Internal Server Error");
            }
        }
    }
}
