using Microsoft.AspNetCore.Mvc;
using Moq;
using TasksManager.Controllers;
using TasksManager.DataAccess;
using TasksManager.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace TaskManagerTestProject
{
    [TestClass]
    public class TestTasksManagerController
    {
        private Mock<IConnectDB> _connectDB = new Mock<IConnectDB>();

        [TestMethod]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            //arrange
            var tasksManager = new TasksManagerController(_connectDB.Object);

            //setup
            _connectDB.Setup(x => x.ReadTasks()).Returns(GetTestTasks());

            //act
            Task<IActionResult> response = tasksManager.GetAllTasks();

            //assert
            Assert.AreEqual(200, ((ObjectResult)response.Result).StatusCode);
            Assert.AreEqual(GetResponse(GetTestTasks()), ((ObjectResult)response.Result).Value);
        }
        [TestMethod]
        public void AddTask_ShouldAddTaskSuccessfully()
        {
            //arrange
            //One task with the due date in the future
            Taskitem testTask = new Taskitem { Id = 1, Name = "Carwash", Description = "Take car to carwash", DueDate = new DateTime(2023, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority = PriorityEnum.High, Status = StatusEnum.New };

            string json = JsonConvert.SerializeObject(testTask);

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(json);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var httpContext = new DefaultHttpContext()
            {
                Request = { Body = stream, ContentLength = stream.Length }
            };
            var controllerContext = new ControllerContext { HttpContext = httpContext };

            //setup
            _connectDB.Setup(x => x.InsertTask(testTask));
            _connectDB.Setup(x => x.ReadTasks()).Returns(GetTestTasks());

            var tasksManager = new TasksManagerController(_connectDB.Object){ ControllerContext = controllerContext };

            //act
            Task<IActionResult> response = tasksManager.AddTask();

            //assert
            Assert.AreEqual(200, ((ObjectResult)response.Result).StatusCode);
            Assert.AreEqual("A new task added", ((ObjectResult)response.Result).Value);
        }
        [TestMethod]
        public void AddTask_ShouldNOTAddTaskSuccessfully()
        {
            //arrange
            //One task with the due date in the past
            Taskitem testTask = new Taskitem { Id = 1, Name = "Carwash", Description = "Take car to carwash", DueDate = new DateTime(2021, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority = PriorityEnum.High, Status = StatusEnum.New };

            string json = JsonConvert.SerializeObject(testTask);

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(json);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var httpContext = new DefaultHttpContext()
            {
                Request = { Body = stream, ContentLength = stream.Length }
            };
            var controllerContext = new ControllerContext { HttpContext = httpContext };

            //setup
            _connectDB.Setup(x => x.InsertTask(testTask));
            _connectDB.Setup(x => x.ReadTasks()).Returns(GetTestTasks());

            var tasksManager = new TasksManagerController(_connectDB.Object) { ControllerContext = controllerContext };

            //act
            Task<IActionResult> response = tasksManager.AddTask();

            //assert
            Assert.AreEqual(400, ((ObjectResult)response.Result).StatusCode);
            Assert.AreEqual("Due data can not be in the past", ((ObjectResult)response.Result).Value);
        }

        [TestMethod]
        public void UpdateTask_ShouldUpdateTaskSuccessfully()
        {
            //arrange
            //One task with the due date in the future
            Taskitem testTask = new Taskitem { Id = 1, Name = "Carwash", Description = "Take car to carwash", DueDate = new DateTime(2023, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority = PriorityEnum.High, Status = StatusEnum.New };

            string json = JsonConvert.SerializeObject(testTask);

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(json);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var httpContext = new DefaultHttpContext()
            {
                Request = { Body = stream, ContentLength = stream.Length }
            };
            var controllerContext = new ControllerContext { HttpContext = httpContext };

            //setup
            _connectDB.Setup(x => x.UpdateTask(testTask));
            _connectDB.Setup(x => x.ReadTasks()).Returns(GetTestTasks());

            var tasksManager = new TasksManagerController(_connectDB.Object) { ControllerContext = controllerContext };

            //act
            Task<IActionResult> response = tasksManager.UpdateTask();

            //assert
            Assert.AreEqual(200, ((ObjectResult)response.Result).StatusCode);
            Assert.AreEqual("The task has been updated", ((ObjectResult)response.Result).Value);
        }
        [TestMethod]
        public void UpdateTask_ShouldNOTUpdateTaskNOID()
        {
            //arrange
            //One task with an invalid id
            Taskitem testTask = new Taskitem { Id = -1, Name = "Carwash", Description = "Take car to carwash", DueDate = new DateTime(2023, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority = PriorityEnum.High, Status = StatusEnum.New };

            string json = JsonConvert.SerializeObject(testTask);

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(json);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var httpContext = new DefaultHttpContext()
            {
                Request = { Body = stream, ContentLength = stream.Length }
            };
            var controllerContext = new ControllerContext { HttpContext = httpContext };

            //setup
            _connectDB.Setup(x => x.UpdateTask(testTask));
            _connectDB.Setup(x => x.ReadTasks()).Returns(GetTestTasks());

            var tasksManager = new TasksManagerController(_connectDB.Object) { ControllerContext = controllerContext };

            //act
            Task<IActionResult> response = tasksManager.UpdateTask();

            //assert
            Assert.AreEqual(400, ((ObjectResult)response.Result).StatusCode);
            Assert.AreEqual("There is no task with the current id in the queue.", ((ObjectResult)response.Result).Value);
        }
        private List<Taskitem> GetTestTasks()
        {
            var testTasks = new List<Taskitem>();
            testTasks.Add(new Taskitem { Id = 1, Name = "Carwash", Description = "Take car to carwash", DueDate = new DateTime(2022, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority= PriorityEnum.High, Status= StatusEnum.New });
            testTasks.Add(new Taskitem { Id = 2, Name = "HouseClean", Description = "Clean the house", DueDate = new DateTime(2022, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority = PriorityEnum.High, Status = StatusEnum.New });
            testTasks.Add(new Taskitem { Id = 3, Name = "KidsSport", Description = "Take kids to sport", DueDate = new DateTime(2022, 08, 14, 09, 15, 00), StartDate = new DateTime(2022, 08, 14, 09, 15, 00), EndDate = new DateTime(2022, 08, 14, 09, 15, 00), Priority = PriorityEnum.High, Status = StatusEnum.New });

            return testTasks;
        }
        private string GetResponse(List<Taskitem> results)
        {
            return JsonConvert.SerializeObject(results);
        }
    }
}