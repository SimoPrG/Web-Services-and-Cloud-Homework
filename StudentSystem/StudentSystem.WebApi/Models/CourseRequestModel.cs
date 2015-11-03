namespace StudentSystem.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<int> StudentIds { get; set; }

        public virtual IEnumerable<int> HomeworkIds { get; set; }
    }
}