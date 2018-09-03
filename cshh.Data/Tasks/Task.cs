using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLib;

namespace cshh.Data.Tasks
{
    public interface IUserTask : TaskLib.ITask
    {
        int UserProfile_Id { get; set; }
        User.UserProfile UserProfile { get; set; }
    }

    public class Task : BaseEntity, IUserTask//TaskLib.ITask
    {
        public int TaskId { get => this.Id; set { this.Id = value; } }

        public int UserProfile_Id { get; set; }
        public User.UserProfile UserProfile { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDT { get; set; }
        public DateTime? TargetDT { get; set; }
        public TimeSpan? RemindPeriod { get; set; }
        public long? RemindPeriodTicks { get => RemindPeriod?.Ticks; set => RemindPeriod = value.HasValue ? (TimeSpan?)TimeSpan.FromTicks(value.Value) : null; }

        public bool? IsComplited { get; set; }

        IEnumerable<TaskLib.ITask> ITask.SubTasks { get => SubTasks; set => throw new Exception(); }
        public virtual ICollection<Task> SubTasks { get; set; }

        public int? SupTaskId { get; set; }
        TaskLib.ITask ITask.SupTask { get => SupTask; set => throw new Exception(); }
        public Task SupTask { get; set; }
    }
}
