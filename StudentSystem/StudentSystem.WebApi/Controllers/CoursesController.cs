namespace StudentSystem.WebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Data;
    using Models;
    using StudentSystem.Models;

    public class CoursesController : ApiController
    {
        private readonly IStudentSystemData data;

        public CoursesController()
        {
            this.data = new StudentsSystemData();
        }

        public IHttpActionResult Get()
        {
            var courses = this.data.Courses.All().Select(
                c => new CourseRequestModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    HomeworkIds = c.Homework.Select(h => h.Id),
                    StudentIds = c.Students.Select(s => s.StudentId)
                });

            if (!courses.Any())
            {
                return this.NotFound();
            }

            return this.Ok(courses);
        }

        public IHttpActionResult Get(Guid id)
        {
            var course = this.data.Courses.SearchFor(c => c.Id == id).FirstOrDefault();
            if (course == null)
            {
                return this.NotFound();
            }

            return this.Ok(course);
        }

        public IHttpActionResult Post(CourseRequestModel course)
        {
            if (course == null)
            {
                return this.BadRequest("Course must be set.");
            }
            var c = new Course
            {
                Description = course.Description,
                Name = course.Name
            };

            this.data.Courses.Add(c);
            this.data.SaveChanges();

            return this.Ok(c.Id);
        }

        public IHttpActionResult Put(CourseRequestModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var homeworIds = course?.HomeworkIds ?? new int[] {};
            var studentIds = course?.StudentIds ?? new int[] {};

            var homework =
                this.data.Homework.SearchFor(h => homeworIds.Contains(h.Id)).ToList();
            var students =
                this.data.Students.All().Where(s => studentIds.Contains(s.StudentId)).ToList();

            this.data.Courses.Update(new Course
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Homework = homework,
                Students = students
            });

            this.data.SaveChanges();

            return this.Ok();
        }

        public IHttpActionResult Delete(Guid id)
        {
            var course = this.data.Courses.SearchFor(c => c.Id == id).FirstOrDefault();
            if (course == null)
            {
                return this.NotFound();
            }

            this.data.Courses.Delete(course);
            this.data.SaveChanges();

            return this.Ok();
        }
    }
}
