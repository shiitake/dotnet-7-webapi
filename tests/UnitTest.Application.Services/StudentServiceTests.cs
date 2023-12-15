using Xunit;
using Moq;
using Moq.AutoMock;
using Moq.EntityFrameworkCore;
using Application;
using Core.Domain.Entities;
using Application.Services;

namespace UnitTest.Application.Services
{
    public class StudentServiceTests
    {
        public StudentServiceTests() { }

        [Fact]
        public async Task GetStudentGrades_GivenNoGrades_ShouldReturnEmptyList()
        {
            // Arrange
            var studentDbContextMock = new Mock<SchoolDbContext>();
            studentDbContextMock.Setup(x => x.StudentGrades).ReturnsDbSet(new List<StudentGrade>());
            studentDbContextMock.Setup(x => x.People).ReturnsDbSet(new List<Person>());
            var mocker = new AutoMocker();
            mocker.Use(studentDbContextMock);
            var studentServiceMock = mocker.CreateInstance<StudentService>();
            
            // Act
            var results = await studentServiceMock.GetStudentGrades();

            // Assert
            Assert.NotNull(results);
            Assert.Empty(results);
        }

        [Fact]
        public async Task GetStudentGrades_GivenSingleGrade_ShouldReturnCorrectGpa()
        {
            // Arrange
            var studentGrade = new StudentGrade { StudentId = 1, Grade = 3.5m };
            var person = new Person { PersonId = 1, FirstName = "Joe", LastName = "Tester" };
            
            var studentDbContextMock = new Mock<SchoolDbContext>();
            studentDbContextMock.Setup(x => x.StudentGrades).ReturnsDbSet(new List<StudentGrade> { studentGrade });
            studentDbContextMock.Setup(x => x.People).ReturnsDbSet(new List<Person> { person });
            var mocker = new AutoMocker();
            mocker.Use(studentDbContextMock);
            var studentServiceMock = mocker.CreateInstance<StudentService>();

            // Act
            var results = await studentServiceMock.GetStudentGrades();

            // Assert
            Assert.Single(results);
            Assert.Equal(3.5m, results[0].Gpa);
        }

        [Fact]
        public async Task GetStudentGrades_GivenMultipleGrades_ShouldReturnCorrectAverageGpa()
        {
            // Arrange
            var studentGrades = new List<StudentGrade> 
            {
                new StudentGrade { StudentId = 1, Grade = 3.5m },
                new StudentGrade { StudentId = 1, Grade = 3.0m }
            };
            var person = new Person { PersonId = 1, FirstName = "Joe", LastName = "Tester" };
            var studentDbContextMock = new Mock<SchoolDbContext>();
            studentDbContextMock.Setup(x => x.StudentGrades).ReturnsDbSet(studentGrades);
            studentDbContextMock.Setup(x => x.People).ReturnsDbSet(new List<Person> { person });
            var mocker = new AutoMocker();
            mocker.Use(studentDbContextMock);
            var studentServiceMock = mocker.CreateInstance<StudentService>();

            // Act
            var results = await studentServiceMock.GetStudentGrades();

            // Assert
            Assert.Single(results);
            Assert.Equal(3.25m, results[0].Gpa);
        }

        [Fact]
        public async Task GetStudentGrades_GivenMultipleStudents_ShouldReturnCorrectStudentProfilesWithGpa()
        {
            // Arrange
            var studentGrades = new List<StudentGrade>
            {
                new StudentGrade { StudentId = 1, Grade = 3.5m },
                new StudentGrade { StudentId = 1, Grade = 3.0m },
                new StudentGrade { StudentId = 2, Grade = 3.3m },
                new StudentGrade { StudentId = 2, Grade = 3.1m }
            };
            var people = new List<Person>
            {
                new Person { PersonId = 1, FirstName = "Joe", LastName = "Tester" },
                new Person { PersonId = 2, FirstName = "Jane", LastName = "Tester" } 
            };

            var studentDbContextMock = new Mock<SchoolDbContext>();
            studentDbContextMock.Setup(x => x.StudentGrades).ReturnsDbSet(studentGrades);
            studentDbContextMock.Setup(x => x.People).ReturnsDbSet(people);
            var mocker = new AutoMocker();
            mocker.Use(studentDbContextMock);
            var studentServiceMock = mocker.CreateInstance<StudentService>();

            // Act
            var results = await studentServiceMock.GetStudentGrades();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Equal(3.25m, results!.Find(x => x.StudentId == 1)!.Gpa);
            Assert.Equal(3.2m, results!.Find(x => x.StudentId == 2)!.Gpa);
        }
    }
}