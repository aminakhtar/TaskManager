using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using TasksManager.DataAccess;
using TasksManager.Models;

namespace TasksManager.Controllers
{
    [Route("api/task-management")]
    [ApiController]
    public class TasksManagerController : ControllerBase
    {
        private readonly IConnectDB _connectDB;
        private const int maxHighPriorityTasks = 100;

        public TasksManagerController(IConnectDB connectDB)
        {
            _connectDB = connectDB;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks() 
        {
            var response = new TaskManagementServiceResponse() { StatusCode = 200, StatusMessage = "Get"};

            List<Taskitem> results = _connectDB.ReadTasks();
            response.StatusMessage = JsonConvert.SerializeObject(results);

            return StatusCode(response.StatusCode, response.StatusMessage);
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask()
        {
            var response = new TaskManagementServiceResponse() { StatusCode = 200, StatusMessage = "A new task added" };

            //Getting user's insert task
            string jsonRequest;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                jsonRequest = await reader.ReadToEndAsync();

                if (!string.IsNullOrEmpty(jsonRequest))
                {
                    Taskitem taskitem = JsonConvert.DeserializeObject<Taskitem>(jsonRequest);

                    if(taskitem != null && taskitem.DueDate < DateTime.Now)
                    {
                        response.StatusMessage = "Due data can not be in the past";
                        response.StatusCode = 400;
                        return StatusCode(response.StatusCode, response.StatusMessage);
                    }

                    if(taskitem != null && taskitem.Priority == PriorityEnum.High && !IsDataValid(maxHighPriorityTasks))
                    {
                        response.StatusMessage = "Too many unfinished high priority tasks are in the queue already!";
                        response.StatusCode = 400;
                        return StatusCode(response.StatusCode, response.StatusMessage);
                    }

                    if (taskitem != null)
                    {
                        _connectDB.InsertTask(taskitem);
                    }
                }
            }
            return StatusCode(response.StatusCode, response.StatusMessage);
        }

        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask()
        {
            var response = new TaskManagementServiceResponse() { StatusCode = 200, StatusMessage = "The task has been updated" };

            //Getting user's insert task
            string jsonRequest;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                jsonRequest = await reader.ReadToEndAsync();

                if (!string.IsNullOrEmpty(jsonRequest))
                {
                    Taskitem taskitem = JsonConvert.DeserializeObject<Taskitem>(jsonRequest);

                    List<Taskitem> allTasks = _connectDB.ReadTasks();

                    // If currently there is no task or no task with the id of update task, throw an error
                    if(taskitem == null || allTasks.Count == 0 || !allTasks.Any(x => x.Id == taskitem.Id))
                    {
                        response.StatusCode = 400;
                        response.StatusMessage = "There is no task with the current id in the queue.";
                    }else
                    {
                        _connectDB.UpdateTask(taskitem);
                    }
                }
            }
            return StatusCode(response.StatusCode, response.StatusMessage);
        }

        private bool IsDataValid(int maxHighUnfinishedRecords)
        {
            List<Taskitem> allTasks = _connectDB.ReadTasks();

            var groupByTasksQuery =
                from task in allTasks
                where task.Priority == PriorityEnum.High && task.Status != StatusEnum.Finished
                group task by task.DueDate into newGroup
                where newGroup.Count() >= maxHighUnfinishedRecords
                orderby newGroup.Key
                select newGroup;

            return !groupByTasksQuery.Any();
        }
    }
}
