using Application.Models;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;
        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentViewModel>> GetStudentGrades()
        {
            return await (from sg in _context.StudentGrades
                          join p in _context.People
                          on sg.StudentId equals p.PersonId
                          where sg.Grade != null
                          group new { sg, p } by new { sg.StudentId, p.FirstName, p.LastName } into studentGrades
                          select new StudentViewModel
                          {
                              StudentId = studentGrades.Key.StudentId,
                              FirstName = studentGrades.Key.FirstName,
                              LastName = studentGrades.Key.LastName,
                              Gpa = Math.Round((decimal)studentGrades.Average(x => x.sg.Grade)!, 2, MidpointRounding.ToZero)
                          }).ToListAsync();

        }
    }
}
