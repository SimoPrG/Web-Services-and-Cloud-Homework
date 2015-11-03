namespace StudentSystem.Data
{
    using Repositories;
    using Models;

    public interface IStudentSystemData
    {
        IGenericRepository<Course> Courses { get; }

        IGenericRepository<Student> Students { get; }

        IGenericRepository<Homework> Homework { get; }

        void SaveChanges();
    }
}
