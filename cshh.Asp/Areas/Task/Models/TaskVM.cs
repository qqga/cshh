using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Areas.Task.Models
{
    public class TaskVM
    {
        public int Id { get; set; }

        public int UserProfile_Id { get; set; }
        public UserProfile UserProfile { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDT { get; set; }
        public string CreatedDTStr { get => CreatedDT.ToString(); set => CreatedDT = value == null ? DateTime.MinValue : DateTime.Parse(value); }
        public DateTime? TargetDT { get; set; }
        public string TargetDTStr { get => TargetDT?.ToString() ?? ""; set => TargetDT = value == null ? null : (DateTime?)DateTime.Parse(value); }
        public TimeSpan? RemindPeriod { get; set; }
        public string RemindPeriodStr { get => RemindPeriod?.ToString() ?? ""; set => RemindPeriod = value == null ? null : (TimeSpan?)TimeSpan.Parse(value); }

        public bool? IsComplited { get; set; }

        public ICollection<TaskVM> SubTasks { get; set; } = new List<TaskVM>();

        public int? SupTaskId { get; set; }
        public TaskVM SupTask { get; set; }

    }
}