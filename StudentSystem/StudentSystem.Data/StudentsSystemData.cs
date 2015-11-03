﻿namespace StudentSystem.Data
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Repositories;

    public class StudentsSystemData : IStudentSystemData
    {
        private readonly IStudentSystemDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public StudentsSystemData()
            : this(new StudentSystemDbContext())
        {
        }

        public StudentsSystemData(IStudentSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Course> Courses
        {
            get
            {
                return this.GetRepository<Course>();
            }
        }

        public IGenericRepository<Student> Students
        {
            get
            {
                return this.GetRepository<Student>();
            }
        }

        public IGenericRepository<Homework> Homework
        {
            get
            {
                return this.GetRepository<Homework>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}