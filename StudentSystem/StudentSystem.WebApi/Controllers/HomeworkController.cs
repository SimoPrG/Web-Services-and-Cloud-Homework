namespace StudentSystem.WebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Data;
    using Models;
    using StudentSystem.Models;

    public class HomeworkController : ApiController
    {
        private readonly IStudentSystemData data;

        public HomeworkController()
        {
            this.data = new StudentsSystemData();
        }

        public IHttpActionResult Get()
        {
            var homework = this.data.Homework.All().Select(
                h => new HomeworkRequestModel
                {
                    StudentId = h.StudentIdentification,
                    CourseId = h.CourseId,
                    FileUrl = h.FileUrl,
                    TimeSent = h.TimeSent
                });

            if (!homework.Any())
            {
                return this.NotFound();
            }

            return this.Ok(homework);
        }

        public IHttpActionResult Get(int id)
        {
            var homework = this.data.Homework.SearchFor(h => h.Id == id).FirstOrDefault();
            if (homework == null)
            {
                return this.NotFound();
            }

            var homeworkModel = new HomeworkRequestModel
            {
                StudentId = homework.StudentIdentification,
                CourseId = homework.CourseId,
                FileUrl = homework.FileUrl,
                TimeSent = homework.TimeSent
            };

            return this.Ok(homeworkModel);
        }

        public IHttpActionResult Post(HomeworkRequestModel homework)
        {
            if (homework == null)
            {
                return this.BadRequest("Homework must be set.");
            }
            var h = new Homework
            {
                CourseId = homework.CourseId,
                StudentIdentification = homework.StudentId,
                FileUrl = homework.FileUrl,
                TimeSent = DateTime.Now
            };

            this.data.Homework.Add(h);
            this.data.SaveChanges();

            return this.Ok(h.Id);
        }

        public IHttpActionResult Put(HomeworkRequestModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.data.Homework.Update(new Homework
            {
                Id = homework.Id,
                CourseId = homework.CourseId,
                StudentIdentification = homework.Id,
                FileUrl = homework.FileUrl,
                TimeSent = DateTime.Now
            });

            this.data.SaveChanges();

            return this.Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var homework = this.data.Homework.SearchFor(h => h.Id == id).FirstOrDefault();
            if (homework == null)
            {
                return this.NotFound();
            }

            this.data.Homework.Delete(homework);
            this.data.SaveChanges();

            return this.Ok();
        }

    }
}
