namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Homework> homework;

        public Course()
        {
            this.Id = Guid.NewGuid();
            this.students = new HashSet<Student>();
            this.homework = new HashSet<Homework>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Student> Students
        {
            get
            {
                return this.students;
            }
            set
            {
                this.students = value;
            }
        }

        public virtual ICollection<Homework> Homework
        {
            get
            {
                return this.homework;
            }
            set
            {
                this.homework = value;
            }
        }
    }
}
