namespace StudentSystem.WebApi.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data;
    using Models;
    using StudentSystem.Models;

    public class StudentsController : ApiController
    {
        private readonly IStudentSystemData data;

        public StudentsController()
        {
            this.data = new StudentsSystemData();
        }

        public IHttpActionResult Get()
        {
            var students = this.data.Students.All().Select(
                s => new StudentRequestModel()
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Level = s.Level,
                    Address = s.AdditionalInformation.Address,
                    Email = s.AdditionalInformation.Email
                });

            if (!students.Any())
            {
                return this.NotFound();
            }

            return this.Ok(students);
        }

        public IHttpActionResult Get(int id)
        {
            var student = this.data.Students.SearchFor(s => s.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return this.NotFound();
            }

            return this.Ok(student);
        }

        public IHttpActionResult Post(StudentRequestModel student)
        {
            if (student == null)
            {
                return this.BadRequest("Student must be set.");
            }
            var s = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Level = student.Level,
                AdditionalInformation = new StudentInfo
                {
                    Email = student.Email,
                    Address = student.Address
                }
            };

            this.data.Students.Add(s);
            this.data.SaveChanges();

            return this.Ok(s.StudentId);
        }

        public IHttpActionResult Put(StudentRequestModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.data.Students.Update(new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                AdditionalInformation = new StudentInfo
                {
                    Address = student.Address,
                    Email = student.Email
                },
                Level = student.Level
            });

            this.data.SaveChanges();

            return this.Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var student = this.data.Students.SearchFor(s => s.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return this.NotFound();
            }

            this.data.Students.Delete(student);
            this.data.SaveChanges();

            return this.Ok();
        }
    }
}
