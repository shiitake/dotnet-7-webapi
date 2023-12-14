using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            var response = await _studentService.GetStudentGrades();
            if (response is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        } 
    }
}
