using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Task
{
    #region RegionName
    class TaskMap : BaseEntityTypeConfiguration<cshh.Data.Tasks.Task>
    {

        public TaskMap() : base()
        {
            ToTable("Task", "Task");

            Ignore(t => t.TaskId);
            Ignore(t => t.RemindPeriod);

            Property(t => t.Name).IsRequired().HasMaxLength(200).IsUnicode();
            Property(t => t.Description).IsMaxLength().IsUnicode();

            HasRequired(t => t.UserProfile).WithMany(t => t.Tasks).HasForeignKey(b => b.UserProfile_Id).WillCascadeOnDelete(true);
            Property(t => t.SupTaskId).HasColumnName("SupTask_Id");

            //HasOptional(t => t.SupTask).WithMany(t => t.SubTasks as ICollection<Data.Task.Task>).HasForeignKey(t => t.SupTaskId).WillCascadeOnDelete(true);
            HasOptional(t => t.SupTask).WithMany(t => t.SubTasks).HasForeignKey(t => t.SupTaskId).WillCascadeOnDelete(false);

            //HasMany(t => t.SubTasks as ICollection<Data.Task.Task>).WithOptional(t => t.SupTask as Data.Task.Task).HasForeignKey(t => t.SupTaskId);
        }
    }
    #endregion
}
