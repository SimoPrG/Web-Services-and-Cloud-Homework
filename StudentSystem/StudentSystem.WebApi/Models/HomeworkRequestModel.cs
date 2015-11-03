namespace StudentSystem.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HomeworkRequestModel
    {
        public int Id { get; set; }

        [Required]
        public string FileUrl { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public Guid CourseId { get; set; }
    }
}