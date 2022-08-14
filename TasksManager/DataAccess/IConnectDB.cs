using TasksManager.Models;

namespace TasksManager.DataAccess
{
    public interface IConnectDB
    {
        public List<Taskitem> ReadTasks();
        public void InsertTask(Taskitem taskitem);
        public void UpdateTask(Taskitem taskitem);
    }
}
